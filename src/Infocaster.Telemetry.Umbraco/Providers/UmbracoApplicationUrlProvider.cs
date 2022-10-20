using Microsoft.Extensions.Options;
using System.Collections.Generic;
using Umbraco.Cms.Core.Configuration.Models;
using Umbraco.Cms.Core.Hosting;

namespace Infocaster.Telemetry.Umbraco.Providers
{
    public class UmbracoApplicationUrlProvider : ITelemetryProvider
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly WebRoutingSettings _webRoutingSettings;

        public UmbracoApplicationUrlProvider(
            IHostingEnvironment hostingEnvironment,
            IOptions<WebRoutingSettings> webRoutingSettings)
        {
            _hostingEnvironment = hostingEnvironment;
            _webRoutingSettings = webRoutingSettings.Value;
        }

        public IEnumerable<IAppTelemetry> GetTelemetry()
        {
            yield return new AppTelemetry<string>("Umbraco.ApplicationUrl", _hostingEnvironment.ApplicationMainUrl.ToString());
            yield return new AppTelemetry<string>("Umbraco.ApplicationUrlSetting", _webRoutingSettings.UmbracoApplicationUrl);
        }
    }
}