using System.Collections.Generic;
using Umbraco.Core;

namespace Infocaster.Telemetry.Umbraco.Providers
{
    public class UmbracoDebugModeProvider : ITelemetryProvider
    {
        private readonly IRuntimeState _runtimeState;

        public UmbracoDebugModeProvider(IRuntimeState runtimeState)
        {
            _runtimeState = runtimeState;
        }

        public IEnumerable<IAppTelemetry> GetTelemetry()
        {
            yield return new AppTelemetry<bool>("Umbraco.DebugMode", _runtimeState.Debug);
        }
    }
}