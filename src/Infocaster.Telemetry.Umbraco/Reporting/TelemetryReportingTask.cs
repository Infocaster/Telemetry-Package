using Infocaster.Telemetry.Umbraco.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Umbraco.Cms.Core;
using Umbraco.Cms.Core.Logging;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Infrastructure.HostedServices;

namespace Infocaster.Telemetry.Umbraco.Reporting
{
    /// <summary>
    /// Recurring task that sends telemetry reports to the telemetry report api endpoint.
    /// </summary>
    public class TelemetryReportingTask : RecurringHostedServiceBase
    {
        private readonly IRuntimeState _runtimeState;
        private readonly TelemetryReportingConfiguration _telemetryReportingConfiguration;
        private readonly ITelemetryReportProvider _telemetryReportProvider;
        private readonly IEnumerable<ITelemetryReporter> _telemetryReporters;
        private readonly IProfilingLogger _profilingLogger;
        private readonly ILogger<TelemetryReportingTask> _logger;

        public TelemetryReportingTask(
            IRuntimeState runtimeState,
            IOptions<TelemetryReportingConfiguration> telemetryReportingConfiguration,
            ITelemetryReportProvider telemetryReportProvider,
            IEnumerable<ITelemetryReporter> telemetryReporters,
            IProfilingLogger profilingLogger,
            ILogger<TelemetryReportingTask> logger)
                : base(
                      logger,
                      TimeSpan.FromMilliseconds(telemetryReportingConfiguration.Value.PeriodMilliseconds),
                      TimeSpan.FromMilliseconds(telemetryReportingConfiguration.Value.DelayMilliseconds))
        {
            _runtimeState = runtimeState;
            _telemetryReportingConfiguration = telemetryReportingConfiguration.Value;
            _telemetryReportProvider = telemetryReportProvider;
            _telemetryReporters = telemetryReporters;
            _profilingLogger = profilingLogger;
            _logger = logger;
        }

        public override async Task PerformExecuteAsync(object? state)
        {
            if (!_telemetryReportingConfiguration.EnableReporting)
            {
                return;
            }

            if (_runtimeState.Level is not RuntimeLevel.Run)
            {
                return;
            }

            var report = _telemetryReportProvider.GetReport();

            if (report.AppId == default)
            {
                _logger.LogWarning(
                    "Aborting telemetry reporting: app id is missing. " +
                    "Please create an app setting with name \"Telemetry:AppId\" " +
                    "and with a newly created GUID as value.");

                return;
            }

            if (!_telemetryReporters.Any())
            {
                _logger.LogWarning(
                    "Aborting telemetry reporting: no telemetry reporters were resolved");

                return;
            }

            // Iterate telemetry reporters and have them do their telemetry reporting.
            foreach (var telemetryReporter in _telemetryReporters)
            {
                try
                {
                    using (_profilingLogger.TraceDuration(
                        telemetryReporter.GetType(),
                        "Reporting telemetry",
                        "Finished reporting telemetry"))
                    {
                        await telemetryReporter.ReportTelemetry(report);
                    }
                }
                catch (Exception e)
                {
                    _logger.LogError(e, "An error occured while reporting telemetry");
                }
            }
        }
    }
}