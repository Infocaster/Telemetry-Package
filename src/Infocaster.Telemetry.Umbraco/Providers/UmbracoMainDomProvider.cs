using System.Collections.Generic;
using System.Configuration;
using Umbraco.Core;

namespace Infocaster.Telemetry.Umbraco.Providers
{
    public class UmbracoMainDomProvider : ITelemetryProvider
    {
        private readonly IRuntimeState _runtimeState;

        public UmbracoMainDomProvider(IRuntimeState runtimeState)
        {
            _runtimeState = runtimeState;
        }

        public IEnumerable<IAppTelemetry> GetTelemetry()
        {
            yield return new AppTelemetry<bool>("Umbraco.IsMainDom", _runtimeState.IsMainDom);
            yield return new AppTelemetry<string>("Umbraco.MainDomLockSetting", ConfigurationManager.AppSettings["Umbraco.Core.MainDom.Lock"]);
        }
    }
}