using System;
using System.Collections.Generic;
using Umbraco.Cms.Core.Logging.Viewer;

namespace Infocaster.Telemetry.Umbraco.Providers
{
    public class UmbracoLogErrorCountProvider : ITelemetryProvider
    {
        private readonly ILogViewer _logViewer;

        public UmbracoLogErrorCountProvider(ILogViewer logViewer)
        {
            _logViewer = logViewer;
        }

        public IEnumerable<IAppTelemetry> GetTelemetry()
        {
            var startTime = DateTime.UtcNow.Date.AddDays(-1);
            var endTime = DateTime.UtcNow.Date;
            var period = new LogTimePeriod(startTime, endTime);
            var counts = _logViewer.GetLogLevelCounts(period);
            yield return new AppTelemetry<int>("Umbraco.Logs.Debug.Count", counts.Debug);
            yield return new AppTelemetry<int>("Umbraco.Logs.Information.Count", counts.Information);
            yield return new AppTelemetry<int>("Umbraco.Logs.Warning.Count", counts.Warning);
            yield return new AppTelemetry<int>("Umbraco.Logs.Error.Count", counts.Error);
            yield return new AppTelemetry<int>("Umbraco.Logs.Fatal.Count", counts.Fatal);
        }
    }
}