using System.Collections.Generic;
using Umbraco.Core.Composing;

namespace Infocaster.Telemetry.Umbraco.Composing
{
    /// <summary>
    /// Telemetry reporter collection builder.
    /// </summary>
    public class TelemetryReporterCollectionBuilder : LazyCollectionBuilderBase<TelemetryReporterCollectionBuilder, TelemetryReporterCollection, ITelemetryReporter>
    {
        protected override TelemetryReporterCollectionBuilder This => this;
    }

    /// <summary>
    /// Telemetry reporter collection.
    /// </summary>
    public class TelemetryReporterCollection : BuilderCollectionBase<ITelemetryReporter>
    {
        public TelemetryReporterCollection(IEnumerable<ITelemetryReporter> items)
            : base(items)
        {

        }
    }
}