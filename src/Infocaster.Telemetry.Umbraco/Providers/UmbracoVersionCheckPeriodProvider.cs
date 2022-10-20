using System.Collections.Generic;
using Umbraco.Core.Configuration;

namespace Infocaster.Telemetry.Umbraco.Providers
{
    public class UmbracoVersionCheckPeriodProvider : ITelemetryProvider
    {
        private readonly IGlobalSettings _globalSettings;

        public UmbracoVersionCheckPeriodProvider(IGlobalSettings globalSettings)
        {
            _globalSettings = globalSettings;
        }

        public IEnumerable<IAppTelemetry> GetTelemetry()
        {
            yield return new AppTelemetry<int>("Umbraco.VersionCheckPeriod", _globalSettings.VersionCheckPeriod);
        }
    }
}