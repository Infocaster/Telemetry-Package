using System;
using System.Collections.Generic;

namespace Infocaster.Telemetry.Umbraco.Providers
{
    public class TargetFrameworkProvider : ITelemetryProvider
    {
        public IEnumerable<IAppTelemetry> GetTelemetry()
        {
            yield return new AppTelemetry<string>("System.TargetFramework", AppDomain.CurrentDomain.SetupInformation.TargetFrameworkName!);
        }
    }
}