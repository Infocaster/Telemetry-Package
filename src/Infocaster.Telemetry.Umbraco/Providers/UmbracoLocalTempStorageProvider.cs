using System.Collections.Generic;
using System.Configuration;

namespace Infocaster.Telemetry.Umbraco.Providers
{
    public class UmbracoLocalTempStorageProvider : ITelemetryProvider
    {
        public IEnumerable<IAppTelemetry> GetTelemetry()
        {
            var umbracoLocalTempStorage = ConfigurationManager.AppSettings["umbracoLocalTempStorage"];
            var umbracoContentXMLStorage = ConfigurationManager.AppSettings["umbracoContentXMLStorage"];
            var umbracoContentXMLUseLocalTempSetting = ConfigurationManager.AppSettings["umbracoContentXMLUseLocalTemp"];
            var umbracoContentXMLUseLocalTemp =
                string.Equals(umbracoContentXMLUseLocalTempSetting, "true")
                    ? "AspNetTemp"
                    : null;
            yield return new AppTelemetry<string>("Umbraco.LocalTempStorageLocation",
                umbracoLocalTempStorage ?? umbracoContentXMLStorage ?? umbracoContentXMLUseLocalTemp ?? "Default");
        }
    }
}