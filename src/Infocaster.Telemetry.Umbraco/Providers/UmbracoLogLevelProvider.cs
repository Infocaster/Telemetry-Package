using System.Collections.Generic;
using Umbraco.Cms.Core.Logging.Viewer;

namespace Infocaster.Telemetry.Umbraco.Providers
{
    public class UmbracoLogLevelProvider : ITelemetryProvider
    {
        private readonly ILogLevelLoader _logLevelLoader;

        public UmbracoLogLevelProvider(ILogLevelLoader logLevelLoader)
        {
            _logLevelLoader = logLevelLoader;
        }

        public IEnumerable<IAppTelemetry> GetTelemetry()
        {
            var logLevel = _logLevelLoader.GetGlobalMinLogLevel();
            var logLevelString = logLevel?.ToString();
            if (logLevelString is null) yield break;
            yield return new AppTelemetry<string>("Umbraco.Logs.LogLevel", logLevelString);
        }
    }
}