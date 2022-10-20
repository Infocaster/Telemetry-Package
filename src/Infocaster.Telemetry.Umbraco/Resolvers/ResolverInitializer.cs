using Infocaster.Telemetry.Umbraco.Extensions;
using Umbraco.Core;

namespace Infocaster.Telemetry.Umbraco.Resolvers
{
    /// <summary>
    /// Initializes resolvers for all implementations of <see cref="ITelemetryReporter"/> and <see cref="ITelemetryProvider"/>.
    /// </summary>
    public class ResolverInitializer : ApplicationEventHandler
    {
        protected override void ApplicationInitialized(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
        {
            TelemetryReporterResolver.Current = new TelemetryReporterResolver(
                applicationContext.ProfilingLogger.Logger,
                PluginManager.Current.ResolveTelemetryReporters());

            TelemetryProviderResolver.Current = new TelemetryProviderResolver(
                applicationContext.ProfilingLogger.Logger,
                PluginManager.Current.ResolveTelemetryProviders());
        }
    }
}