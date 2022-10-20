using System.Collections.Generic;
using Umbraco.Core.Configuration;

namespace Infocaster.Telemetry.Umbraco.Providers
{
    public class UmbracoLocalTempStorageProvider : ITelemetryProvider
    {
        private readonly IGlobalSettings _globalSettings;

        public UmbracoLocalTempStorageProvider(IGlobalSettings globalSettings)
        {
            _globalSettings = globalSettings;
        }

        public IEnumerable<IAppTelemetry> GetTelemetry()
        {
            yield return new AppTelemetry<string>("Umbraco.LocalTempPath", _globalSettings.LocalTempPath);
            yield return new AppTelemetry<string>("Umbraco.LocalTempStorageLocation", _globalSettings.LocalTempStorageLocation.ToString());
        }
    }
}