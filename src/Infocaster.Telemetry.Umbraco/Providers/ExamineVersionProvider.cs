using Examine;
using System.Collections.Generic;

namespace Infocaster.Telemetry.Umbraco.Providers
{
    public class ExamineVersionProvider : ITelemetryProvider
    {
        public IEnumerable<IAppTelemetry> GetTelemetry()
        {
            yield return new AppTelemetry<string>("Umbraco.Examine.ExamineVersion", typeof(ExamineManager).Assembly.GetName().Version.ToString());
        }
    }
}