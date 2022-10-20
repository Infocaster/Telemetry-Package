using Examine;
using Examine.LuceneEngine.Providers;
using System.Collections.Generic;
using Umbraco.Core;

namespace Infocaster.Telemetry.Umbraco.Providers
{
    public class ExamineIndexItemCountProvider : ITelemetryProvider
    {
        public IEnumerable<IAppTelemetry> GetTelemetry()
        {
            yield return new AppTelemetry<int>("Umbraco.Examine.ExternalIndexItemCount", GetIndexItemCount(Constants.Examine.ExternalIndexer));
            yield return new AppTelemetry<int>("Umbraco.Examine.InternalIndexItemCount", GetIndexItemCount(Constants.Examine.InternalIndexer));
            yield return new AppTelemetry<int>("Umbraco.Examine.MembersIndexItemCount", GetIndexItemCount(Constants.Examine.InternalMemberIndexer));
        }

        private int GetIndexItemCount(string indexerName)
        {
            var indexer = ExamineManager.Instance.IndexProviderCollection[indexerName] as LuceneIndexer;
            if (indexer == null) return 0;
            using (var reader = indexer.GetIndexWriter().GetReader())
                return reader.NumDocs();
        }
    }
}