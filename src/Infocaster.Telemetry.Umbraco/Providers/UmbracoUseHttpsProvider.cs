using System.Collections.Generic;
using umbraco;

namespace Infocaster.Telemetry.Umbraco.Providers
{
    public class UmbracoUseHttpsProvider : ITelemetryProvider
    {
        public IEnumerable<IAppTelemetry> GetTelemetry()
        {
            yield return new AppTelemetry<bool>("Umbraco.UseHttps", GlobalSettings.UseSSL);
        }
    }
}