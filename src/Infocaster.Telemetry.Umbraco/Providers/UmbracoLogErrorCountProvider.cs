using System;
using System.Collections.Generic;
using Umbraco.Core.Logging.Viewer;

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
            var counts = GetLogLevelCounts(startTime, endTime);
            yield return new AppTelemetry<int>("Umbraco.Logs.Debug.Count", counts.Debug);
            yield return new AppTelemetry<int>("Umbraco.Logs.Information.Count", counts.Information);
            yield return new AppTelemetry<int>("Umbraco.Logs.Warning.Count", counts.Warning);
            yield return new AppTelemetry<int>("Umbraco.Logs.Error.Count", counts.Error);
            yield return new AppTelemetry<int>("Umbraco.Logs.Fatal.Count", counts.Fatal);
        }

        /// <summary>
        /// This method ensures forward-compatibility for umbraco version 8.1.0 or later. As of umbraco version 8.1.0 the 
        /// method signature of ILogViewer.GetLogLevelCounts is changed from ILogViewer.GetLogLevelCounts(DateTimeOffset, DateTimeOffset) 
        /// to ILogViewer.GetLogLevelCounts(LogTimePeriod). Reflection is used to invoke the changed method signature and to avoid
        /// <see cref="MissingMethodException"/>.
        /// </summary>
        private LogLevelCounts GetLogLevelCounts(DateTime startTime, DateTime endTime)
        {
            var logTimePeriodType = Type.GetType("Umbraco.Core.Logging.Viewer.LogTimePeriod, Umbraco.Core");
            if (logTimePeriodType != null)
            {
                // Umbraco version >= 8.1.0
                var ctor = logTimePeriodType.GetConstructor(new[] { typeof(DateTime), typeof(DateTime) });
                var logTimePeriod = ctor.Invoke(new object[] { startTime, endTime });
                var logViewerType = _logViewer.GetType();
                var method = logViewerType.GetMethod("GetLogLevelCounts");
                return (LogLevelCounts)method.Invoke(_logViewer, new object[] { logTimePeriod });
            }
            else
            {
                // Umbraco version < 8.1.0
                var logViewerType = _logViewer.GetType();
                var method = logViewerType.GetMethod("GetLogLevelCounts");
                return (LogLevelCounts)method.Invoke(_logViewer, new object[] { (DateTimeOffset)startTime, (DateTimeOffset)endTime });
            }
        }
    }
}