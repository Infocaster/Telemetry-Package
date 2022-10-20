using Microsoft.Extensions.Options;
using System.Collections.Generic;
using Umbraco.Cms.Core.Configuration.Models;

namespace Infocaster.Telemetry.Umbraco.Providers
{
    public class UmbracoUseHttpsProvider : ITelemetryProvider
    {
        private readonly GlobalSettings _globalSettings;

        public UmbracoUseHttpsProvider(IOptions<GlobalSettings> globalSettings)
        {
            _globalSettings = globalSettings.Value;
        }

        public IEnumerable<IAppTelemetry> GetTelemetry()
        {
            yield return new AppTelemetry<bool>("Umbraco.UseHttps", _globalSettings.UseHttps);
        }
    }
}