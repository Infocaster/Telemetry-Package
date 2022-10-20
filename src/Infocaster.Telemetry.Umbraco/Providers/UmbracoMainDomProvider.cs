using Microsoft.Extensions.Options;
using System.Collections.Generic;
using Umbraco.Cms.Core.Configuration.Models;
using Umbraco.Cms.Core.Runtime;

namespace Infocaster.Telemetry.Umbraco.Providers
{
    public class UmbracoMainDomProvider : ITelemetryProvider
    {
        private readonly IMainDom _mainDom;
        private readonly GlobalSettings _globalSettings;

        public UmbracoMainDomProvider(
            IMainDom mainDom,
            IOptions<GlobalSettings> globalSettings)
        {
            _mainDom = mainDom;
            _globalSettings = globalSettings.Value;
        }

        public IEnumerable<IAppTelemetry> GetTelemetry()
        {
            yield return new AppTelemetry<bool>("Umbraco.IsMainDom", _mainDom.IsMainDom);
            yield return new AppTelemetry<string>("Umbraco.MainDomLockSetting", _globalSettings.MainDomLock);
        }
    }
}