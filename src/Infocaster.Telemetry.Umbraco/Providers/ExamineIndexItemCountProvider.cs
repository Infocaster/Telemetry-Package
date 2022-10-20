using Examine;
using System;
using System.Collections.Generic;
using Umbraco.Cms.Core;
using Umbraco.Cms.Infrastructure.Examine;

namespace Infocaster.Telemetry.Umbraco.Providers
{
    public class ExamineIndexItemCountProvider : ITelemetryProvider
    {
        private readonly IExamineManager _examineManager;

        public ExamineIndexItemCountProvider(IExamineManager examineManager)
        {
            _examineManager = examineManager;
        }

        public IEnumerable<IAppTelemetry> GetTelemetry()
        {
            yield return new AppTelemetry<int>("Umbraco.Examine.ExternalIndexItemCount", GetIndexItemCount(Constants.UmbracoIndexes.ExternalIndexName));
            yield return new AppTelemetry<int>("Umbraco.Examine.InternalIndexItemCount", GetIndexItemCount(Constants.UmbracoIndexes.InternalIndexName));
            yield return new AppTelemetry<int>("Umbraco.Examine.MembersIndexItemCount", GetIndexItemCount(Constants.UmbracoIndexes.MembersIndexName));
        }

        private int GetIndexItemCount(string indexName)
        {
            if (!_examineManager.TryGetIndex(indexName, out var index) || index is not IIndexDiagnostics indexDiagnostics) return 0;
            var itemCount = indexDiagnostics.GetDocumentCount();
            return Convert.ToInt32(itemCount);
        }
    }
}