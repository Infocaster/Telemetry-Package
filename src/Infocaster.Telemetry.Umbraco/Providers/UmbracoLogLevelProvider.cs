using log4net;
using log4net.Repository.Hierarchy;
using System.Collections.Generic;

namespace Infocaster.Telemetry.Umbraco.Providers
{
    public class UmbracoLogLevelProvider : ITelemetryProvider
    {
        public IEnumerable<IAppTelemetry> GetTelemetry()
        {
            var rootLogger = ((Hierarchy)LogManager.GetRepository()).Root;
            yield return new AppTelemetry<string>("Umbraco.Logs.LogLevel", rootLogger.Level.ToString());
        }
    }
}