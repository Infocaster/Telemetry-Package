using System.Collections.Generic;
using System.Linq;
using Umbraco.Cms.Core.Services;

namespace Infocaster.Telemetry.Umbraco.Providers
{
    public class UmbracoDomainProvider : ITelemetryProvider
    {
        private readonly IDomainService _domainService;

        public UmbracoDomainProvider(IDomainService domainService)
        {
            _domainService = domainService;
        }

        public IEnumerable<IAppTelemetry> GetTelemetry()
        {
            var domains = _domainService.GetAll(true).ToList();
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