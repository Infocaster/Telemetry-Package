using System;
using System.Collections.Generic;

namespace Infocaster.Telemetry.Umbraco
{
    public class AppTelemetryReport
    {
        /// <summary>
        /// Guid that uniquely identifies the telemetry report source application.
        /// </summary>
        public Guid AppId { get; }

        /// <summary>
        /// Preferred display name of the telemetry report source application.
        /// </summary>
        public string AppName { get; }

        /// <summary>
        /// Collection of telemetry to report.
        /// </summary>
        public List<IAppTelemetry> Telemetry { get; }

        public AppTelemetryReport(Guid appId, string appName = null)
        {
            AppId = appId;
            AppName = appName;
            Telemetry = new List<IAppTelemetry>();
        }
    }
}