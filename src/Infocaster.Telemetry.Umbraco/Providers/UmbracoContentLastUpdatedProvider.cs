using System;
using System.Collections.Generic;
using System.Linq;
using Umbraco.Cms.Core.Web;
using Umbraco.Extensions;

namespace Infocaster.Telemetry.Umbraco.Providers
{
    public class UmbracoContentLastUpdatedProvider : ITelemetryProvider
    {
        private readonly IUmbracoContextFactory _umbracoContextFactory;

        public UmbracoContentLastUpdatedProvider(IUmbracoContextFactory umbracoContextFactory)
        {
            _umbracoContextFactory = umbracoContextFactory;
        }

        public IEnumerable<IAppTelemetry> GetTelemetry()
        {
            var dt = GetContentLastUpdatedDate();
            if (dt is null) yield break;
            yield return new AppTelemetry<DateTime>($"Umbraco.Content.LastUpdatedDate", dt.Value.ToUniversalTime());
        }

        /// <summary>
        /// Iterating all content is not great performance-wise but getting update dates from examine was unreliable.
        /// </summary>
        private DateTime? GetContentLastUpdatedDate()
        {
            using var context = _umbracoContextFactory.EnsureUmbracoContext();
            var lastUpdatedDate = context.UmbracoContext.Content?.GetAtRoot().SelectMany(x => x.Descendants()).Max(x => x.UpdateDate);
            if (lastUpdatedDate is null) return null;
            var local = DateTime.SpecifyKind(lastUpdatedDate.Value, DateTimeKind.Local);
            return local;
        }
    }
}