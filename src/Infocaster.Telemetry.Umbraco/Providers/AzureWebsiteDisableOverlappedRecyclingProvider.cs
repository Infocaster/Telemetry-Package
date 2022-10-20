using System.Collections.Generic;
using System.Configuration;

namespace Infocaster.Telemetry.Umbraco.Providers
{
    public class AzureWebsiteDisableOverlappedRecyclingProvider : ITelemetryProvider
    {
        public IEnumerable<IAppTelemetry> GetTelemetry()
        {
            yield return new AppTelemetry<string>("Azure.WebsiteDisableOverlappedRecycling", ConfigurationManager.AppSettings["WEBSITE_DISABLE_OVERLAPPED_RECYCLING"]);
        }
    }
}