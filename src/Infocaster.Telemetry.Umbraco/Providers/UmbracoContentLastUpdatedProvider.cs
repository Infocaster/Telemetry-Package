using System;
using System.Collections.Generic;
using System.Linq;
using Umbraco.Web;

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
            if (dt == null) yield break;
            yield return new AppTelemetry<DateTime>($"Umbraco.Content.LastUpdatedDate", dt.Value.ToUniversalTime());
        }

        /// <summary>
        /// Iterating all content is not great performance-wise but getting update dates from examine was unreliable.
        /// </summary>
        private DateTime? GetContentLastUpdatedDate()
        {
            using (var context = _umbracoContextFactory.EnsureUmbracoContext())
            {
                var content = context.UmbracoContext.ContentCache.GetByXPath("//*[@updateDate]");
                var lastUpdatedContent = content.OrderByDescending(x => x.UpdateDate).FirstOrDefault();
                if (lastUpdatedContent?.UpdateDate == null) return null;
                var local = DateTime.SpecifyKind(lastUpdatedContent.UpdateDate, DateTimeKind.Local);
                return local;
            }
        }
    }
}