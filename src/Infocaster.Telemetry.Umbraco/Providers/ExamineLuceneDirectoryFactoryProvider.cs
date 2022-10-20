using Examine.Config;
using System.Collections.Generic;
using System.Configuration;

namespace Infocaster.Telemetry.Umbraco.Providers
{
    public class ExamineLuceneDirectoryFactoryProvider : ITelemetryProvider
    {
        public IEnumerable<IAppTelemetry> GetTelemetry()
        {
            foreach (ProviderSettings indexer in ExamineSettings.Instance.IndexProviders.Providers)
            {
                var directoryFactory = indexer.Parameters["directoryFactory"];
                if (directoryFactory == null) continue;
                yield return new AppTelemetry<string>("Umbraco.Examine.LuceneDirectoryFactory", directoryFactory);
                yield break;
            }
        }
    }
}