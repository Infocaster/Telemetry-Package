using System.Collections.Generic;
using umbraco;

namespace Infocaster.Telemetry.Umbraco.Providers
{
    public class UmbracoVersionCheckPeriodProvider : ITelemetryProvider
    {
        public IEnumerable<IAppTelemetry> GetTelemetry()
        {
            yield return new AppTelemetry<int>("Umbraco.VersionCheckPeriod", GlobalSettings.VersionCheckPeriod);
        }
    }
}