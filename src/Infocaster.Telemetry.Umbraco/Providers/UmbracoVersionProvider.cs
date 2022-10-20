using System.Collections.Generic;
using Umbraco.Core;
using Umbraco.Core.Configuration;

namespace Infocaster.Telemetry.Umbraco.Providers
{
    public class UmbracoVersionProvider : ITelemetryProvider
    {
        public IEnumerable<IAppTelemetry> GetTelemetry()
        {
            yield return new AppTelemetry<string>("Umbraco.UmbracoVersion", UmbracoVersion.SemanticVersion.ToSemanticString());
        }
    }
}