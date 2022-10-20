namespace Infocaster.Telemetry.Umbraco
{
    /// <summary>
    /// Provides application telemetry reports.
    /// </summary>
    public interface ITelemetryReportProvider
    {
        AppTelemetryReport GetReport();
    }
}