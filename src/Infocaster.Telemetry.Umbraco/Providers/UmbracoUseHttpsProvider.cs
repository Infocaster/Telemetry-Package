using System.Collections.Generic;
using Umbraco.Core.Configuration;

namespace Infocaster.Telemetry.Umbraco.Providers
{
    public class UmbracoUseHttpsProvider : ITelemetryProvider
    {
        private readonly IGlobalSettings _globalSettings;

        public UmbracoUseHttpsProvider(IGlobalSettings globalSettings)
        {
            _globalSettings = globalSettings;
        }

        public IEnumerable<IAppTelemetry> GetTelemetry()
        {
            yield return new AppTelemetry<bool>("Umbraco.UseHttps", _globalSettings.UseHttps);
        }
    }
}