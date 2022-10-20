using Infocaster.Telemetry.Umbraco.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Umbraco.Core.Logging;
using Umbraco.Web.Scheduling;

namespace Infocaster.Telemetry.Umbraco.Reporting
{
    /// <summary>
    /// Recurring task that iterates all telemetry reporters and calls their ITelemetryReporter.ReportTelemetry method.
    /// </summary>
    public class TelemetryReportingTask : RecurringTaskBase
    {
        private readonly ITelemetryReportingConfiguration _telemetryReportingConfiguration;
        private readonly ITelemetryReportProvider _telemetryReportProvider;
        private readonly IEnumerable<ITelemetryReporter> _telemetryReporters;
        private readonly IProfilingLogger _profilingLogger;

        public TelemetryReportingTask(
            ITelemetryReportingConfiguration telemetryReportingConfiguration,
            IBackgroundTaskRunner<RecurringTaskBase> runner,
            ITelemetryReportProvider telemetryReportProvider,
            IEnumerable<ITelemetryReporter> telemetryReporters,
            IProfilingLogger profilingLogger)
                : base(runner, telemetryReportingConfiguration.DelayMilliseconds, telemetryReportingConfiguration.PeriodMilliseconds)
        {
            _telemetryReportingConfiguration = telemetryReportingConfiguration;
            _telemetryReportProvider = telemetryReportProvider;
            _telemetryReporters = telemetryReporters;
            _profilingLogger = profilingLogger;
        }

        public override async Task<bool> PerformRunAsync(CancellationToken token)
        {
            if (!_telemetryReportingConfiguration.EnableReporting)
            {
                // Return false to cancel this run and to stop repeating this task.
                return false;
            }

            var report = _telemetryReportProvider.GetReport();

            if (report.AppId == default)
            {
                _profilingLogger.Warn<TelemetryReportingTask>(
                    "Aborting telemetry reporting: app id is missing. " +
                    "Please create an app setting with name \"Telemetry:AppId\" " +
                    "and with a newly created GUID as value.");

                // Abort telemetry report and stop repeating this task.
                return false;
            }

            if (!_telemetryReporters.Any())
            {
                _profilingLogger.Warn<TelemetryReportingTask>(
                    "Aborting telemetry reporting: no telemetry reporters were resolved");

                // Abort telemetry report and stop repeating this task.
                return false;
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
                        await telemetryReporter.ReportTelemetry(report, token);
                    }
                }
                catch (Exception e)
                {
                    _profilingLogger.Error(
                        telemetryReporter.GetType(),
                        e,
                        "An error occured while reporting telemetry");
                }
            }

            // Return true to continue repeating this task.
            return true;
        }

        public override bool IsAsync => true;
    }
}