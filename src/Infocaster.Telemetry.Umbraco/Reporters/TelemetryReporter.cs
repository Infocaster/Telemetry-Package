using Infocaster.Telemetry.Umbraco.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Infocaster.Telemetry.Umbraco.Reporters
{
    /// <summary>
    /// Default telemetry reporter. Reports telemetry by posting telemetry reports as json to an api endpoint.
    /// </summary>
    public class TelemetryReporter : ITelemetryReporter
    {
        private readonly TelemetryReportingConfiguration _configuration;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<TelemetryReporter> _logger;

        public TelemetryReporter(
            IOptions<TelemetryReportingConfiguration> configuration,
            IHttpClientFactory httpClientFactory,
            ILogger<TelemetryReporter> logger)
        {
            _configuration = configuration.Value;
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }

        /// <summary>
        /// Creates a http request message for the api endpoint using method POST and with a telemetry report serialized to json as content.
        /// </summary>
        public virtual HttpRequestMessage CreateHttpRequestMessage(AppTelemetryReport report)
        {
            var json = JsonConvert.SerializeObject(report);
            var uri = new Uri(_configuration.ApiEndpoint!);
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
            if (string.IsNullOrEmpty(_configuration.ApiAuthHeaderName)) return;
            if (string.IsNullOrEmpty(_configuration.ApiAuthHeaderValue)) return;
            request.Headers.Add(_configuration.ApiAuthHeaderName, _configuration.ApiAuthHeaderValue);
        }

        public virtual async Task ReportTelemetry(AppTelemetryReport report)
        {
            if (_configuration.ApiEndpoint == null)
            {
                _logger.LogWarning(
                    "Aborting telemetry report: api endpoint is not configured. " +
                    "Please create an app setting with name \"Telemetry:ApiEndpoint\" " +
                    "and with the url of your api endpoint as value.");

                return;
            }

            HttpResponseMessage? response = null;

            try
            {
                var request = CreateHttpRequestMessage(report);
                var httpClient = _httpClientFactory.CreateClient(Defaults.HttpClient.Name);
                response = await httpClient.SendAsync(request);
                response.EnsureSuccessStatusCode();
                _logger.LogInformation(
                    "Telemetry report with app id {appId} was sent to {apiEndpoint}",
                    report.AppId,
                    _configuration.ApiEndpoint);
            }
            catch (Exception e)
            {
                _logger.LogError(
                    e,
                    "An error occured while sending a telemetry report to {apiEndpoint}. " +
                    "Response status code is {statusCode} and response reason phrase is {reasonPhrase}. " +
                    "Trying again in {periodMilliseconds}ms.",
                    _configuration.ApiEndpoint,
                    response?.StatusCode,
                    response?.ReasonPhrase,
                    _configuration.PeriodMilliseconds);
            }
        }
    }
}