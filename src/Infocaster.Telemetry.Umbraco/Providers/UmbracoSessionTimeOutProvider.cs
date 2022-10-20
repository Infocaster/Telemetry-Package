using System.Collections.Generic;
using umbraco;

namespace Infocaster.Telemetry.Umbraco.Providers
{
    public class UmbracoSessionTimeOutProvider : ITelemetryProvider
    {
        public IEnumerable<IAppTelemetry> GetTelemetry()
        {
            yield return new AppTelemetry<int>("Umbraco.SessionTimeOutInMinutes", GlobalSettings.TimeOutInMinutes);
        }
    }
}