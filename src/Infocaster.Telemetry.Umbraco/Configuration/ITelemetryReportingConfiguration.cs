namespace Infocaster.Telemetry.Umbraco.Configuration
{
    /// <summary>
    /// Provides telemetry reporting configuration.
    /// </summary>
    public interface ITelemetryReportingConfiguration
    {
        /// <summary>
        /// Api endpoint url to send telemetry reports to.
        /// </summary>
        string ApiEndpoint { get; }

        /// <summary>
        /// Name of the http request header for authorizing with the api, for example: "Authorization".
        /// </summary>
        string ApiAuthHeaderName { get; }

        /// <summary>
        /// Value of the http request header for authorizing with the api, for example: "Basic XXXXXXXX".
        /// </summary>
        string ApiAuthHeaderValue { get; }

        /// <summary>
        /// Delay in milliseconds after app start for sending the initial telemetry report.
        /// </summary>
        int DelayMilliseconds { get; }

        /// <summary>
        /// Period in milliseconds between sending telemetry reports.
        /// </summary>
        int PeriodMilliseconds { get; }

        /// <summary>
        /// Setting indicating if telemetry reporting is enabled.
        /// </summary>
        bool EnableReporting { get; }

        /// <summary>
        /// Preferred display name of the application in telemetry reports.
        /// </summary>
        string AppName { get; }
    }
}