using System.Collections.Generic;
using Umbraco.Cms.Core.Logging.Viewer;

namespace Infocaster.Telemetry.Umbraco.Providers
{
    public class UmbracoLogLevelProvider : ITelemetryProvider
    {
        private readonly ILogViewer _logViewer;

        public UmbracoLogLevelProvider(ILogViewer logViewer)
        {
            _logViewer = logViewer;
        }

        public IEnumerable<IAppTelemetry> GetTelemetry()
        {
            var level = _logViewer.GetLogLevel();
            yield return new AppTelemetry<string>("Umbraco.Logs.LogLevel", level);
        }
    }
}