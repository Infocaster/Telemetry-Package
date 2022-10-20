using Microsoft.Extensions.Options;
using System.Collections.Generic;
using Umbraco.Cms.Core.Configuration.Models;

namespace Infocaster.Telemetry.Umbraco.Providers
{
    public class ExamineLuceneDirectoryFactoryProvider : ITelemetryProvider
    {
        private readonly IndexCreatorSettings _indexCreatorSettings;

        public ExamineLuceneDirectoryFactoryProvider(IOptions<IndexCreatorSettings> indexCreatorSettings)
        {
            _indexCreatorSettings = indexCreatorSettings.Value;
        }

        public IEnumerable<IAppTelemetry> GetTelemetry()
        {
            yield return new AppTelemetry<string>("Umbraco.Examine.LuceneDirectoryFactory", _indexCreatorSettings.LuceneDirectoryFactory.ToString());
        }
    }
}