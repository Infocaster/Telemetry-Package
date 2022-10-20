using Microsoft.Extensions.Options;
using System.Collections.Generic;
using Umbraco.Cms.Core.Configuration.Models;

namespace Infocaster.Telemetry.Umbraco.Providers
{
    public class UmbracoVersionCheckPeriodProvider : ITelemetryProvider
    {
        private readonly GlobalSettings _globalSettings;

        public UmbracoVersionCheckPeriodProvider(IOptions<GlobalSettings> globalSettings)
        {
            _globalSettings = globalSettings.Value;
        }

        public IEnumerable<IAppTelemetry> GetTelemetry()
        {
            yield return new AppTelemetry<int>("Umbraco.VersionCheckPeriod", _globalSettings.VersionCheckPeriod);
        }
    }
}