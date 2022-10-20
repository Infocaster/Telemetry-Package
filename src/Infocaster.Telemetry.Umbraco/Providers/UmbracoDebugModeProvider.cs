using System.Collections.Generic;
using umbraco;

namespace Infocaster.Telemetry.Umbraco.Providers
{
    public class UmbracoDebugModeProvider : ITelemetryProvider
    {
        public IEnumerable<IAppTelemetry> GetTelemetry()
        {
            yield return new AppTelemetry<bool>("Umbraco.DebugMode", GlobalSettings.DebugMode);
        }
    }
}