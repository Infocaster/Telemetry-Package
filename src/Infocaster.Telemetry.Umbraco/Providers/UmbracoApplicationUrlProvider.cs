using System.Collections.Generic;
using Umbraco.Core.Configuration;
using Umbraco.Core.Sync;

namespace Infocaster.Telemetry.Umbraco.Providers
{
    public class UmbracoApplicationUrlProvider : ITelemetryProvider
    {
        public IEnumerable<IAppTelemetry> GetTelemetry()
        {
            // Hacky method of getting the umbraco application url.
            // Sadly ApplicationContext.UmbracoApplicationUrl is not public.
            var serverRegistrar = new SingleServerRegistrar();
            var applicationUrl = serverRegistrar.GetCurrentServerUmbracoApplicationUrl();
            var settings = UmbracoConfig.For.UmbracoSettings();
            yield return new AppTelemetry<string>("Umbraco.ApplicationUrl", applicationUrl);
            yield return new AppTelemetry<string>("Umbraco.ApplicationUrlSetting", settings.WebRouting.UmbracoApplicationUrl);
        }
    }
}