using System.Collections.Generic;

namespace Infocaster.Telemetry.Umbraco
{
    /// <summary>
    /// Provides application telemetry.
    /// </summary>
    public interface ITelemetryProvider
    {
        IEnumerable<IAppTelemetry> GetTelemetry();
    }
}