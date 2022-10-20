using Microsoft.Extensions.Options;
using System.Collections.Generic;
using Umbraco.Cms.Core.Configuration.Models;

namespace Infocaster.Telemetry.Umbraco.Providers
{
    public class UmbracoSessionTimeOutProvider : ITelemetryProvider
    {
        private readonly GlobalSettings _globalSettings;

        public UmbracoSessionTimeOutProvider(IOptions<GlobalSettings> globalSettings)
        {
            _globalSettings = globalSettings.Value;
        }

        public IEnumerable<IAppTelemetry> GetTelemetry()
        {
            yield return new AppTelemetry<int>("Umbraco.SessionTimeOutInMinutes", (int)_globalSettings.TimeOut.TotalMinutes);
        }
    }
}