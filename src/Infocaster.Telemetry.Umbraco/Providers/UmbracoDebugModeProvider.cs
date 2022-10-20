using Microsoft.Extensions.Options;
using System.Collections.Generic;
using Umbraco.Cms.Core.Configuration.Models;

namespace Infocaster.Telemetry.Umbraco.Providers
{
    public class UmbracoDebugModeProvider : ITelemetryProvider
    {
        private readonly HostingSettings _hostingSettings;

        public UmbracoDebugModeProvider(IOptions<HostingSettings> hostingSettings)
        {
            _hostingSettings = hostingSettings.Value;
        }

        public IEnumerable<IAppTelemetry> GetTelemetry()
        {
            yield return new AppTelemetry<bool>("Umbraco.DebugMode", _hostingSettings.Debug);
        }
    }
}