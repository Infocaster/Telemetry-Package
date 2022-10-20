using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace Infocaster.Telemetry.Umbraco.Providers
{
    public class AzureWebsiteDisableOverlappedRecyclingProvider : ITelemetryProvider
    {
        private readonly IConfiguration _configuration;

        public AzureWebsiteDisableOverlappedRecyclingProvider(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IEnumerable<IAppTelemetry> GetTelemetry()
        {
            yield return new AppTelemetry<string>("Azure.WebsiteDisableOverlappedRecycling", _configuration["WEBSITE_DISABLE_OVERLAPPED_RECYCLING"]);
        }
    }
}