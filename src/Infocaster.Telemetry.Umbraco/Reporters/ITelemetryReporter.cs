using System.Threading.Tasks;

namespace Infocaster.Telemetry.Umbraco
{
    /// <summary>
    /// Provides reporting of application telemetry.
    /// </summary>
    public interface ITelemetryReporter
    {
        /// <summary>
        /// Performs reporting of a telemetry report.
        /// </summary>
        Task ReportTelemetry(AppTelemetryReport report);
    }
}