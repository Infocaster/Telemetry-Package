using System;
using System.Collections.Generic;
using Umbraco.Core;

namespace Infocaster.Telemetry.Umbraco.Extensions
{
    public static class PluginManagerExtensions
    {
        /// <summary>
        /// Returns all available telemetry reporter types.
        /// </summary>
        public static IEnumerable<Type> ResolveTelemetryReporters(this PluginManager resolver)
        {
            return resolver.ResolveTypes<ITelemetryReporter>();
        }

        /// <summary>
        /// Returns all available telemetry provider types.
        /// </summary>
        public static IEnumerable<Type> ResolveTelemetryProviders(this PluginManager resolver)
        {
            return resolver.ResolveTypes<ITelemetryProvider>();
        }
    }
}