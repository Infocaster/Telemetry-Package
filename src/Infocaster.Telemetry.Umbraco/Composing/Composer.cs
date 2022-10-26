using Infocaster.Telemetry.Umbraco.Configuration;
using Infocaster.Telemetry.Umbraco.Providers;
using Infocaster.Telemetry.Umbraco.Reporters;
using Infocaster.Telemetry.Umbraco.Reporting;
using Umbraco.Core;
using Umbraco.Core.Composing;

namespace Infocaster.Telemetry.Umbraco.Composing
{
    [RuntimeLevel(MinLevel = RuntimeLevel.Run)]
    public class Composer : IUserComposer
    {
        public void Compose(Composition composition)
        {
            composition.Register<IAppIdentifierProvider, AppIdentifierProvider>();
            composition.Register<ITelemetryReportProvider, TelemetryReportProvider>();
            composition.Register<ITelemetryReportingConfiguration, TelemetryReportingConfiguration>(Lifetime.Singleton);
            // Telemetry reporters
            composition.TelemetryReporters().Add<TelemetryReporter>();
            // Telemetry providers
            composition.TelemetryProviders().Add<AzureWebsiteDisableOverlappedRecyclingProvider>();
            composition.TelemetryProviders().Add<ExamineIndexItemCountProvider>();
            composition.TelemetryProviders().Add<ExamineLuceneDirectoryFactoryProvider>();
            composition.TelemetryProviders().Add<ExamineVersionProvider>();
            composition.TelemetryProviders().Add<UmbracoApplicationUrlProvider>();
            composition.TelemetryProviders().Add<UmbracoContentLastUpdatedProvider>();
            composition.TelemetryProviders().Add<UmbracoDebugModeProvider>();
            composition.TelemetryProviders().Add<UmbracoDomainProvider>();
            composition.TelemetryProviders().Add<UmbracoLocalTempStorageProvider>();
            composition.TelemetryProviders().Add<UmbracoLogErrorCountProvider>();
            composition.TelemetryProviders().Add<UmbracoLogLevelProvider>();
            composition.TelemetryProviders().Add<UmbracoMainDomProvider>();
            composition.TelemetryProviders().Add<UmbracoSessionTimeOutProvider>();
            composition.TelemetryProviders().Add<UmbracoUseHttpsProvider>();
            composition.TelemetryProviders().Add<UmbracoUserLastLoginDateProvider>();
            composition.TelemetryProviders().Add<UmbracoVersionCheckPeriodProvider>();
            composition.TelemetryProviders().Add<UmbracoVersionProvider>();
            composition.TelemetryProviders().Add<TargetFrameworkProvider>();
            // Components
            composition.Components().Append<TelemetryReportingComponent>();
        }
    }

    public static class CompositionExtensions
    {
        /// <summary>
        /// Telemetry reporter collection builder. Use to add or remove telemetry reporters.
        /// </summary>
        public static TelemetryReporterCollectionBuilder TelemetryReporters(this Composition composition)
            => composition.WithCollectionBuilder<TelemetryReporterCollectionBuilder>();

        /// <summary>
        /// Telemetry provider collection builder. Use to add or remove telemetry providers.
        /// </summary>
        public static TelemetryProviderCollectionBuilder TelemetryProviders(this Composition composition)
            => composition.WithCollectionBuilder<TelemetryProviderCollectionBuilder>();
    }
}