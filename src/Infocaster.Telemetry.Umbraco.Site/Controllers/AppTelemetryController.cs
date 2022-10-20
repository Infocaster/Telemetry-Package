using System.Web.Http;
using Umbraco.Web.WebApi;

namespace Infocaster.Telemetry.Umbraco.Controllers
{
    /// <summary>
    /// Controller for easily accessing and testing telemetry report providers.
    /// </summary>
    public class AppTelemetryController : UmbracoApiController
    {
        private readonly ITelemetryReportProvider _telemetryReportProvider;

        public AppTelemetryController(
            ITelemetryReportProvider telemetryReportProvider)
        {
            _telemetryReportProvider = telemetryReportProvider;
        }

        [HttpGet]
        public IHttpActionResult Get()
        {
            var model = _telemetryReportProvider.GetReport();
            return Json(model);
        }
    }
}