using Examine;
using System;
using System.Collections.Generic;
using Umbraco.Examine;

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
            yield return new AppTelemetry<int>("Umbraco.Examine.ExternalIndexItemCount", GetIndexItemCount("ExternalIndex"));
            yield return new AppTelemetry<int>("Umbraco.Examine.InternalIndexItemCount", GetIndexItemCount("InternalIndex"));
            yield return new AppTelemetry<int>("Umbraco.Examine.MembersIndexItemCount", GetIndexItemCount("MembersIndex"));
        }

        private int GetIndexItemCount(string indexName)
        {
            if (!_examineManager.TryGetIndex(indexName, out var index)) return 0;
            var indexDiagnostics = index as IIndexDiagnostics;
            if (indexDiagnostics == null) return 0;
            var itemCount = indexDiagnostics.DocumentCount;
            return Convert.ToInt32(itemCount);
        }
    }
}