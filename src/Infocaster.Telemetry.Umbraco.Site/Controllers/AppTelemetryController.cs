using Infocaster.Telemetry.Umbraco.Configuration;
using Infocaster.Telemetry.Umbraco.Reporting;
using Infocaster.Telemetry.Umbraco.Resolvers;
using System.Web.Http;
using Umbraco.Web.WebApi;

namespace Infocaster.Telemetry.Umbraco.Site.Controllers
{
    /// <summary>
    /// Controller for easily accessing and testing telemetry.
    /// </summary>
    public class AppTelemetryController : UmbracoApiController
    {
        [HttpGet]
        public IHttpActionResult Get()
        {
            var telemetryReportProvider = new TelemetryReportProvider(
                new AppIdentifierProvider(),
                TelemetryProviderResolver.Current.TelemetryProviders,
                ConfigurationProvider.TelemetryReportingConfiguration,
                Logger);
            var report = telemetryReportProvider.GetReport();
            return Json(report);
        }
    }
}