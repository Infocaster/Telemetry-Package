using System;

namespace Infocaster.Telemetry.Umbraco.Configuration
{
    /// <summary>
    /// Provides lazily initialized configuration.
    /// </summary>
    public static class ConfigurationProvider
    {
        private static readonly Lazy<ITelemetryReportingConfiguration> _telemetryReportingConfiguration
            = new Lazy<ITelemetryReportingConfiguration>(() => new TelemetryReportingConfiguration());

        /// <summary>
        /// Telemetry reporting configuration.
        /// </summary>
        public static ITelemetryReportingConfiguration TelemetryReportingConfiguration
            => _telemetryReportingConfiguration.Value;
    }
}