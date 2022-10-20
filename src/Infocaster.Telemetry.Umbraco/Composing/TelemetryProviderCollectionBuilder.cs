using System.Collections.Generic;
using Umbraco.Core.Composing;

namespace Infocaster.Telemetry.Umbraco.Composing
{
    /// <summary>
    /// Telemetry provider collection builder.
    /// </summary>
    public class TelemetryProviderCollectionBuilder : LazyCollectionBuilderBase<TelemetryProviderCollectionBuilder, TelemetryProviderCollection, ITelemetryProvider>
    {
        protected override TelemetryProviderCollectionBuilder This => this;
    }

    /// <summary>
    /// Telemetry provider collection.
    /// </summary>
    public class TelemetryProviderCollection : BuilderCollectionBase<ITelemetryProvider>
    {
        public TelemetryProviderCollection(IEnumerable<ITelemetryProvider> items)
            : base(items)
        {

        }
    }
}