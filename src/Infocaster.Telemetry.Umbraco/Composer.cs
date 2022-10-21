using Infocaster.Telemetry.Umbraco.Configuration;
using Infocaster.Telemetry.Umbraco.Providers;
using Infocaster.Telemetry.Umbraco.Reporters;
using Infocaster.Telemetry.Umbraco.Reporting;
using Microsoft.Extensions.DependencyInjection;
using Umbraco.Cms.Core.Composing;
using Umbraco.Cms.Core.DependencyInjection;

namespace Infocaster.Telemetry.Umbraco
{
    public class Composer : IComposer
    {
        public void Compose(IUmbracoBuilder builder)
        {
            // Options
            var configuration = builder.Config.GetSection(Defaults.Options.TelemetrySection);
            builder.Services
                .AddOptions<TelemetryReportingConfiguration>()
                .Bind(configuration)
                .ValidateDataAnnotations();
            // Services
            builder.Services.AddTransient<ITelemetryReportProvider, TelemetryReportProvider>();
            builder.Services.AddTransient<ITelemetryReporter, TelemetryReporter>();
            builder.Services.AddTransient<ITelemetryProvider, AzureWebsiteDisableOverlappedRecyclingProvider>();
            builder.Services.AddTransient<ITelemetryProvider, ExamineIndexItemCountProvider>();
            builder.Services.AddTransient<ITelemetryProvider, ExamineLuceneDirectoryFactoryProvider>();
            builder.Services.AddTransient<ITelemetryProvider, ExamineVersionProvider>();
            builder.Services.AddTransient<ITelemetryProvider, UmbracoApplicationUrlProvider>();
            builder.Services.AddTransient<ITelemetryProvider, UmbracoContentLastUpdatedProvider>();
            builder.Services.AddTransient<ITelemetryProvider, UmbracoDebugModeProvider>();
            builder.Services.AddTransient<ITelemetryProvider, UmbracoDomainProvider>();
            builder.Services.AddTransient<ITelemetryProvider, UmbracoLocalTempStorageProvider>();
            builder.Services.AddTransient<ITelemetryProvider, UmbracoLogErrorCountProvider>();
            builder.Services.AddTransient<ITelemetryProvider, UmbracoLogLevelProvider>();
            builder.Services.AddTransient<ITelemetryProvider, UmbracoMainDomProvider>();
            builder.Services.AddTransient<ITelemetryProvider, UmbracoSessionTimeOutProvider>();
            builder.Services.AddTransient<ITelemetryProvider, UmbracoUseHttpsProvider>();
            builder.Services.AddTransient<ITelemetryProvider, UmbracoUserLastLoginDateProvider>();
            builder.Services.AddTransient<ITelemetryProvider, UmbracoVersionCheckPeriodProvider>();
            builder.Services.AddTransient<ITelemetryProvider, UmbracoVersionProvider>();
            builder.Services.AddTransient<ITelemetryProvider, TargetFrameworkProvider>();
            // Hosted services
            builder.Services.AddHostedService<TelemetryReportingTask>();
            // Http client factory
            builder.Services.AddHttpClient(Defaults.HttpClient.Name);
        }
    }
}