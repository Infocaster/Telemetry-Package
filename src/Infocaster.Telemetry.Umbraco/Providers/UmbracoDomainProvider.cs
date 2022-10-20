using System.Collections.Generic;
using System.Linq;
using Umbraco.Core;
using Umbraco.Core.Services;
using Umbraco.Web;

namespace Infocaster.Telemetry.Umbraco.Providers
{
    public class UmbracoDomainProvider : ITelemetryProvider
    {
        private IDomainService DomainService
            => ApplicationContext.Current.Services.DomainService;

        public IEnumerable<IAppTelemetry> GetTelemetry()
        {
            var domains = DomainService.GetAll(true).ToList();
            var grouping = domains.GroupBy(x => x.LanguageIsoCode);
            foreach (var group in grouping)
            {
                var languageIsoCode = group.Key;
                var domainNames = group.Select(x => x.DomainName);
                yield return new AppTelemetry<string>($"Umbraco.Domains.{languageIsoCode}", string.Join(",", domainNames));
            }
            yield return new AppTelemetry<int>("Umbraco.Domains.Count", domains.Count());
        }
    }
}