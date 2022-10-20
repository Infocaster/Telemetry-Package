using Infocaster.Telemetry.Umbraco.Configuration;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Umbraco.Core;
using Umbraco.Core.Logging;

namespace Infocaster.Telemetry.Umbraco.Reporting
{
    /// <summary>
    /// Default telemetry reporter. Reports telemetry by posting telemetry reports as json to an api endpoint.
    /// </summary>
    public class TelemetryReporter : ITelemetryReporter
    {
        private static readonly HttpClient _httpClient = new HttpClient();

        private readonly ITelemetryReportingConfiguration _configuration;

        private readonly ILogger _logger;

        public TelemetryReporter()
            : this(ConfigurationProvider.TelemetryReportingConfiguration, ApplicationContext.Current.ProfilingLogger.Logger)
        {

        }

        public TelemetryReporter(
            ITelemetryReportingConfiguration configuration,
            ILogger logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        /// <summary>
        /// Creates a http request message for the api endpoint using method POST and with a telemetry report serialized to json as content.
        /// </summary>
        public virtual HttpRequestMessage CreateHttpRequestMessage(AppTelemetryReport report)
        {
            var json = JsonConvert.SerializeObject(report);
            var uri = new Uri(_configuration.ApiEndpoint);
            var request = new HttpRequestMessage(HttpMethod.Post, uri);
            request.Content = new StringContent(json, Encoding.UTF8, "application/json");
            SetRequestAuthHeader(request);
            return request;
        }

        /// <summary>
        /// Sets the authorization header for the http request message. No header is set if the header name and header value are not configured.
        /// </summary>
        public virtual void SetRequestAuthHeader(HttpRequestMessage request)
        {
            if (_configuration.ApiAuthHeaderName == null) return;
            if (_configuration.ApiAuthHeaderValue == null) return;
            request.Headers.Add(_configuration.ApiAuthHeaderName, _configuration.ApiAuthHeaderValue);
        }

        public virtual async Task ReportTelemetry(AppTelemetryReport report, CancellationToken token)
        {
            if (_configuration.ApiEndpoint == null)
            {
                _logger.Warn<TelemetryReporter>(
                    "Aborting telemetry report: api endpoint is not configured. " +
                    "Please create an app setting with name \"Telemetry:ReportingApiEndpoint\" " +
                    "and with the url of your api endpoint as value.");

                return;
            }

            HttpResponseMessage response = null;

            try
            {
                var request = CreateHttpRequestMessage(report);
                response = await _httpClient.SendAsync(request, token);
                response.EnsureSuccessStatusCode();
                _logger.Info<TelemetryReporter>(
                    "Telemetry report with app id {0} was sent to {1}",
                    () => report.AppId,
                    () => _configuration.ApiEndpoint);
            }
            catch (Exception e)
            {
                var message = string.Format(
                    "An error occured while sending a telemetry report to {0}. " +
                    "Response status code is {1} and response reason phrase is {2}. " +
                    "Trying again in {3}ms.",
                    _configuration.ApiEndpoint,
                    response?.StatusCode,
                    response?.ReasonPhrase,
                    _configuration.PeriodMilliseconds);
                _logger.Error<TelemetryReporter>(message, e);
            }
        }
    }
}