using System.Collections.Generic;
using System.Configuration;

namespace Infocaster.Telemetry.Umbraco.Providers
{
    public class ExamineLuceneDirectoryFactoryProvider : ITelemetryProvider
    {
        public IEnumerable<IAppTelemetry> GetTelemetry()
        {
            yield return new AppTelemetry<string>("Umbraco.Examine.LuceneDirectoryFactory", ConfigurationManager.AppSettings["Umbraco.Examine.LuceneDirectoryFactory"]);
        }
    }
}