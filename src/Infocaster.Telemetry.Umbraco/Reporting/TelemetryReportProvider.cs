using Infocaster.Telemetry.Umbraco.Configuration;
using System;
using System.Collections.Generic;
using Umbraco.Core.Logging;

namespace Infocaster.Telemetry.Umbraco.Reporting
{
    /// <summary>
    /// Collects telemetry from all registered telemetry providers into telemetry reports.
    /// </summary>
    public class TelemetryReportProvider : ITelemetryReportProvider
    {
        private readonly IAppIdentifierProvider _appIdentifierProvider;
        private readonly IEnumerable<ITelemetryProvider> _telemetryProviders;
        private readonly ITelemetryReportingConfiguration _telemetryReportingConfiguration;
        private readonly ILogger _logger;

        public TelemetryReportProvider(
            IAppIdentifierProvider appIdentifierProvider,
            IEnumerable<ITelemetryProvider> telemetryProviders,
            ITelemetryReportingConfiguration telemetryReportingConfiguration,
            ILogger logger)
        {
            _appIdentifierProvider = appIdentifierProvider;
            _telemetryProviders = telemetryProviders;
            _telemetryReportingConfiguration = telemetryReportingConfiguration;
            _logger = logger;
        }

        public AppTelemetryReport GetReport()
        {
            var appId = _appIdentifierProvider.GetAppId();
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
                    _logger.Error<TelemetryReportProvider>("An error occured while collecting application telemetry", e);
                }
            }
            return report;
        }
    }
}