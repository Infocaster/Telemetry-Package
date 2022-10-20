using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Umbraco.Cms.Web.Common.Controllers;

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
        public IActionResult Get()
        {
            var model = _telemetryReportProvider.GetReport();
            var json = JsonConvert.SerializeObject(model);
            return Content(json, "application/json");
        }
    }
}