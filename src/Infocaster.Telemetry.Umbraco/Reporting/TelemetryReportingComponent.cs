using Infocaster.Telemetry.Umbraco.Configuration;
using System.Collections.Generic;
using Umbraco.Core.Composing;
using Umbraco.Core.Logging;
using Umbraco.Web.Scheduling;

namespace Infocaster.Telemetry.Umbraco.Reporting
{
    /// <summary>
    /// Component that enqueues a recurring telemetry reporting task.
    /// </summary>
    public class TelemetryReportingComponent : IComponent
    {
        private readonly ITelemetryReportingConfiguration _telemetryReportingConfiguration;
        private readonly ITelemetryReportProvider _telemetryReportProvider;
        private readonly IEnumerable<ITelemetryReporter> _telemetryReporters;
        private readonly IProfilingLogger _logger;
        private readonly BackgroundTaskRunner<IBackgroundTask> _telemetryReportingRunner;

        public TelemetryReportingComponent(
            ITelemetryReportingConfiguration telemetryReportingConfiguration,
            ITelemetryReportProvider telemetryReportProvider,
            IEnumerable<ITelemetryReporter> telemetryReporters,
            IProfilingLogger logger)
        {
            _telemetryReportingConfiguration = telemetryReportingConfiguration;
            _telemetryReportProvider = telemetryReportProvider;
            _telemetryReporters = telemetryReporters;
            _logger = logger;
            _telemetryReportingRunner = new BackgroundTaskRunner<IBackgroundTask>("TelemetryReporting", _logger);
        }

        public void Initialize()
        {
            var task = new TelemetryReportingTask(
                _telemetryReportingConfiguration,
                _telemetryReportingRunner,
                _telemetryReportProvider,
                _telemetryReporters,
                _logger);
            _telemetryReportingRunner.TryAdd(task);
        }

        public void Terminate()
        {
            // the AppDomain / maindom / whatever takes care of stopping background task runners
        }
    }
}