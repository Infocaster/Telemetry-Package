using System;
using System.Configuration;

namespace Infocaster.Telemetry.Umbraco.Configuration
{
    /// <summary>
    /// Provides configuration of telemetry reporting using app settings.
    /// </summary>
    public class TelemetryReportingConfiguration : ITelemetryReportingConfiguration
    {
        /// <summary>
        /// Default to 1 minute.
        /// </summary>
        private const int _delayMillisecondsDefault = 60 * 1000;

        /// <summary>
        /// Default to 24 hours.
        /// </summary>
        private const int _periodMillisecondsDefault = 60 * 1000 * 60 * 24;

        /// <summary>
        /// Telemetry reporting is enabled by default.
        /// </summary>
        private const bool _enableReportingDefault = true;

        /// <summary>
        /// Prefix for telemetry reporting app setting keys.
        /// </summary>
        private const string _appSettingPrefix = "Telemetry:";

        public string ApiEndpoint { get; }
        public string ApiAuthHeaderName { get; }
        public string ApiAuthHeaderValue { get; }
        public int DelayMilliseconds { get; }
        public int PeriodMilliseconds { get; }
        public bool EnableReporting { get; }
        public string AppName { get; }

        public TelemetryReportingConfiguration()
        {
            ApiEndpoint = GetStringAppSetting("ReportingApiEndpoint", null);
            ApiAuthHeaderName = GetStringAppSetting("ReportingApiAuthHeaderName", null);
            ApiAuthHeaderValue = GetStringAppSetting("ReportingApiAuthHeaderValue", null);
            DelayMilliseconds = GetIntAppSetting("ReportingDelayMilliseconds", _delayMillisecondsDefault);
            PeriodMilliseconds = GetIntAppSetting("ReportingPeriodMilliseconds", _periodMillisecondsDefault);
            EnableReporting = GetBoolAppSetting("EnableReporting", _enableReportingDefault);
            AppName = GetStringAppSetting("AppName", null);
        }

        private static string GetAppSetting(string key)
        {
            var inter = ConfigurationManager.AppSettings[_appSettingPrefix + key];
            return inter;
        }

        private static string GetStringAppSetting(string key, string defaultValue)
        {
            var inter = GetAppSetting(key);
            if (string.IsNullOrEmpty(inter)) return defaultValue;
            return inter;
        }

        private static int GetIntAppSetting(string key, int defaultValue)
        {
            var inter = GetAppSetting(key);
            return int.TryParse(inter, out int value) ? value : defaultValue;
        }

        private static bool GetBoolAppSetting(string key, bool defaultValue)
        {
            var inter = GetAppSetting(key);
            if (string.IsNullOrEmpty(inter)) return defaultValue;
            return string.Equals(inter, "true", StringComparison.InvariantCultureIgnoreCase);
        }
    }
}