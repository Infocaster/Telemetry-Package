using System.Collections.Generic;
using Umbraco.Cms.Core.Configuration;
using Umbraco.Extensions;

namespace Infocaster.Telemetry.Umbraco.Providers
{
    public class UmbracoVersionProvider : ITelemetryProvider
    {
        private readonly IUmbracoVersion _umbracoVersion;

        public UmbracoVersionProvider(IUmbracoVersion umbracoVersion)
        {
            _umbracoVersion = umbracoVersion;
        }

        public IEnumerable<IAppTelemetry> GetTelemetry()
        {
            yield return new AppTelemetry<string>("Umbraco.UmbracoVersion", _umbracoVersion.SemanticVersion.ToSemanticStringWithoutBuild());
        }
    }
}