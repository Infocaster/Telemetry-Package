using System;
using System.Collections.Generic;
using Umbraco.Core.Logging;
using Umbraco.Core.ObjectResolution;
using Umbraco.Web;

namespace Infocaster.Telemetry.Umbraco.Resolvers
{
    /// <summary>
    /// Resolver for all implementations of <see cref="ITelemetryProvider"/>. This resolver can be used at 
    /// application startup to add or remove implementations or replace implementations with your own.
    /// </summary>
    public class TelemetryProviderResolver : ManyObjectsResolverBase<TelemetryProviderResolver, ITelemetryProvider>
    {
        public TelemetryProviderResolver(ILogger logger, IEnumerable<Type> types)
            : base(new TelemetryProviderProvider(), logger, types, ObjectLifetimeScope.Transient)
        {

        }

        /// <summary>
        /// Gets instances of all telemetry provider types. Instances are created by <see cref="TelemetryProviderProvider"/>.
        /// </summary>
        public IEnumerable<ITelemetryProvider> TelemetryProviders
        {
            get { return Values; }
        }

        /// <summary>
        /// Creates instances of telemetry provider types. Provides umbraco context for
        /// telemetry provider types with constructors that need it.
        /// </summary>
        private class TelemetryProviderProvider : IServiceProvider
        {
            public object GetService(Type serviceType)
            {
                var ctor = serviceType.GetConstructor(new[] { typeof(UmbracoContext) });
                return
                    ctor != null
                        ? (ITelemetryProvider)ctor.Invoke(new object[] { UmbracoContext.Current })
                        : (ITelemetryProvider)Activator.CreateInstance(serviceType);
            }
        }
    }
}