using Microsoft.Extensions.Options;
using System.Collections.Generic;
using Umbraco.Cms.Core.Configuration.Models;
using Umbraco.Cms.Core.Hosting;

namespace Infocaster.Telemetry.Umbraco.Providers
{
    public class UmbracoLocalTempStorageProvider : ITelemetryProvider
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly HostingSettings _hostingSettings;

        public UmbracoLocalTempStorageProvider(
            IHostingEnvironment hostingEnvironment,
            IOptions<HostingSettings> hostingSettings)
        {
            _hostingEnvironment = hostingEnvironment;
            _hostingSettings = hostingSettings.Value;
        }

        public IEnumerable<IAppTelemetry> GetTelemetry()
        {
            yield return new AppTelemetry<string>("Umbraco.LocalTempPath", _hostingEnvironment.LocalTempPath);
            yield return new AppTelemetry<string>("Umbraco.LocalTempStorageLocation", _hostingSettings.LocalTempStorageLocation.ToString());
        }
    }
}