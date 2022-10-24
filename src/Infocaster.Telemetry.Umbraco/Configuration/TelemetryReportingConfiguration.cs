using System;

namespace Infocaster.Telemetry.Umbraco.Configuration
{
    /// <summary>
    /// Provides configuration of telemetry reporting.
    /// </summary>
    public class TelemetryReportingConfiguration
    {
        /// <summary>
        /// Default to 1 minute.
        /// </summary>
        private const int _delayMillisecondsDefault = 60 * 1000;

        /// <summary>
        /// Default to 24 hours.
        /// </summary>
        private const int _periodMillisecondsDefault = 60 * 1000 * 60 * 24;

        /// <summary>
        /// Telemetry reporting is enabled by default.
        /// </summary>
        private const bool _enableReportingDefault = true;

        /// <summary>
        /// Api endpoint url to send telemetry reports to.
        /// </summary>
        public string? ApiEndpoint { get; set; }

        /// <summary>
        /// Name of the http request header for authorizing with the api, for example: "Authorization".
        /// </summary>
        public string? ApiAuthHeaderName { get; set; }

        /// <summary>
        /// Value of the http request header for authorizing with the api, for example: "Basic XXXXXXXX".
        /// </summary>
        public string? ApiAuthHeaderValue { get; set; }

        /// <summary>
        /// Delay in milliseconds after app start for sending the initial telemetry report.
        /// </summary>
        public int DelayMilliseconds { get; set; } = _delayMillisecondsDefault;

        /// <summary>
        /// Period in milliseconds between sending telemetry reports.
        /// </summary>
        public int PeriodMilliseconds { get; set; } = _periodMillisecondsDefault;

        /// <summary>
        /// Setting indicating if telemetry reporting is enabled.
        /// </summary>
        public bool EnableReporting { get; set; } = _enableReportingDefault;

        /// <summary>
        /// Guid that uniquely identifies the application.
        /// </summary>
        public Guid? AppId { get; set; } = Guid.Empty;

        /// <summary>
        /// Preferred display name of the application in telemetry reports.
        /// </summary>
        public string? AppName { get; set; }
    }
}