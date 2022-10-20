using System;
using System.Collections.Generic;
using Umbraco.Core.Logging;
using Umbraco.Core.ObjectResolution;
using Umbraco.Web;

namespace Infocaster.Telemetry.Umbraco.Resolvers
{
    /// <summary>
    /// Resolver for all implementations of <see cref="ITelemetryReporter"/>. This resolver can be used at 
    /// application startup to add or remove implementations or replace implementations with your own.
    /// </summary>
    public class TelemetryReporterResolver : ManyObjectsResolverBase<TelemetryReporterResolver, ITelemetryReporter>
    {
        public TelemetryReporterResolver(ILogger logger, IEnumerable<Type> types)
            : base(new TelemetryReporterProvider(), logger, types, ObjectLifetimeScope.Transient)
        {

        }

        /// <summary>
        /// Gets instances of all telemetry provider types. Instances are created by <see cref="TelemetryReporterProvider"/>.
        /// </summary>
        public IEnumerable<ITelemetryReporter> TelemetryReporters
        {
            get { return Values; }
        }

        /// <summary>
        /// Creates instances of telemetry reporter types. Provides umbraco context for
        /// telemetry reporter types with constructors that need it.
        /// </summary>
        private class TelemetryReporterProvider : IServiceProvider
        {
            public object GetService(Type serviceType)
            {
                var ctor = serviceType.GetConstructor(new[] { typeof(UmbracoContext) });
                return
                    ctor != null
                        ? (ITelemetryReporter)ctor.Invoke(new object[] { UmbracoContext.Current })
                        : (ITelemetryReporter)Activator.CreateInstance(serviceType);
            }
        }
    }
}