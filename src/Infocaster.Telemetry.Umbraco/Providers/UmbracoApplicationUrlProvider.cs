using System.Collections.Generic;
using Umbraco.Core;
using Umbraco.Core.Configuration.UmbracoSettings;

namespace Infocaster.Telemetry.Umbraco.Providers
{
    public class UmbracoApplicationUrlProvider : ITelemetryProvider
    {
        private readonly IRuntimeState _runtimeState;
        private readonly IUmbracoSettingsSection _settings;

        public UmbracoApplicationUrlProvider(
            IRuntimeState runtimeState,
            IUmbracoSettingsSection settings)
        {
            _runtimeState = runtimeState;
            _settings = settings;
        }

        public IEnumerable<IAppTelemetry> GetTelemetry()
        {
            yield return new AppTelemetry<string>("Umbraco.ApplicationUrl", _runtimeState.ApplicationUrl?.ToString());
            yield return new AppTelemetry<string>("Umbraco.ApplicationUrlSetting", _settings.WebRouting.UmbracoApplicationUrl);
        }
    }
}