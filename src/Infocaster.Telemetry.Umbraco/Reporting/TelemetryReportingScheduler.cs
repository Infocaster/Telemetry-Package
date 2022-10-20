using Infocaster.Telemetry.Umbraco.Configuration;
using Infocaster.Telemetry.Umbraco.Resolvers;
using System.Threading;
using Umbraco.Core;
using Umbraco.Web.Scheduling;

namespace Infocaster.Telemetry.Umbraco.Reporting
{
    /// <summary>
    /// Scheduler that enqueues a recurring telemetry reporting task.
    /// </summary>
    /// <remarks>
    /// This code is heavily inspired by umbraco's scheduler source code.
    /// </remarks>
    public class TelemetryReportingScheduler : ApplicationEventHandler
    {
        private bool _started = false;
        private object _locker = new object();
        private IBackgroundTask _task;

        protected override void ApplicationStarted(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
        {
            if (umbracoApplication.Context == null)
                return;

            // Backgrounds runners are web aware, if the app domain dies, these tasks will wind down correctly
            var _telemetryReportingRunner = new BackgroundTaskRunner<IBackgroundTask>("TelemetryReporting", applicationContext.ProfilingLogger.Logger);

            // Initialize recurring telemetry reporting task
            LazyInitializer.EnsureInitialized(ref _task, ref _started, ref _locker, () =>
            {
                var task = new TelemetryReportingTask(
                    ConfigurationProvider.TelemetryReportingConfiguration,
                    _telemetryReportingRunner,
                    new TelemetryReportProvider(
                        new AppIdentifierProvider(),
                        TelemetryProviderResolver.Current.TelemetryProviders,
                        ConfigurationProvider.TelemetryReportingConfiguration,
                        applicationContext.ProfilingLogger.Logger),
                    TelemetryReporterResolver.Current.TelemetryReporters,
                    applicationContext.ProfilingLogger);

                // Add telemetry reporting task to the queue
                _telemetryReportingRunner.TryAdd(task);

                return task;
            });
        }
    }
}