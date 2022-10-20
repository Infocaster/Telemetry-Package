using Infocaster.Telemetry.Umbraco.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;

namespace Infocaster.Telemetry.Umbraco.Reporting
{
    /// <summary>
    /// Collects telemetry from all registered telemetry providers into telemetry reports.
    /// </summary>
    public class TelemetryReportProvider : ITelemetryReportProvider
    {
        private readonly IEnumerable<ITelemetryProvider> _telemetryProviders;
        private readonly TelemetryReportingConfiguration _telemetryReportingConfiguration;
        private readonly ILogger<TelemetryReportProvider> _logger;

        public TelemetryReportProvider(
            IEnumerable<ITelemetryProvider> telemetryProviders,
            IOptions<TelemetryReportingConfiguration> telemetryReportingConfiguration,
            ILogger<TelemetryReportProvider> logger)
        {
            _telemetryProviders = telemetryProviders;
            _telemetryReportingConfiguration = telemetryReportingConfiguration.Value;
            _logger = logger;
        }

        public virtual AppTelemetryReport GetReport()
        {
            var appId = _telemetryReportingConfiguration.AppId;
            var appName = _telemetryReportingConfiguration.AppName;
            var report = new AppTelemetryReport(appId, appName);
            foreach (var provider in _telemetryProviders)
            {
                try
                {
                    var telemetry = provider.GetTelemetry();
                    report.Telemetry.AddRange(telemetry);
                }
                catch (Exception e)
                {
                    _logger.LogError(e, "An error occured while collecting application telemetry");
                }
            }
            return report;
        }
    }
}