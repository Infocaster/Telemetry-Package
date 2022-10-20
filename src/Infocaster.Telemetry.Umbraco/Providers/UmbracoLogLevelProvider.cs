using System.Collections.Generic;
using Umbraco.Core.Logging;

namespace Infocaster.Telemetry.Umbraco.Providers
{
    public class UmbracoLogLevelProvider : ITelemetryProvider
    {
        private readonly ILogger _logger;

        public UmbracoLogLevelProvider(ILogger logger)
        {
            _logger = logger;
        }

        public IEnumerable<IAppTelemetry> GetTelemetry()
        {
            var level = GetLogLevel();
            yield return new AppTelemetry<string>("Umbraco.Logs.LogLevel", level.ToString());
        }

        private LogLevel GetLogLevel()
        {
            if (_logger.IsEnabled<UmbracoLogLevelProvider>(LogLevel.Verbose)) return LogLevel.Verbose;
            if (_logger.IsEnabled<UmbracoLogLevelProvider>(LogLevel.Debug)) return LogLevel.Debug;
            if (_logger.IsEnabled<UmbracoLogLevelProvider>(LogLevel.Information)) return LogLevel.Information;
            if (_logger.IsEnabled<UmbracoLogLevelProvider>(LogLevel.Warning)) return LogLevel.Warning;
            if (_logger.IsEnabled<UmbracoLogLevelProvider>(LogLevel.Error)) return LogLevel.Error;
            return LogLevel.Fatal;
        }
    }
}