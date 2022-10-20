using System;
using System.Collections.Generic;
using System.Linq;
using Umbraco.Core;
using Umbraco.Core.Services;
using Umbraco.Web;

namespace Infocaster.Telemetry.Umbraco.Providers
{
    public class UmbracoUserLastLoginDateProvider : ITelemetryProvider
    {
        private IUserService UserService
            => ApplicationContext.Current.Services.UserService;

        public IEnumerable<IAppTelemetry> GetTelemetry()
        {
            var users = UserService.GetAll(0, int.MaxValue, out var totalRecords);
            var grouping = users.GroupBy(x => x.UserType.Name);
            foreach (var group in grouping)
            {
                var usersInGroup = group.Select(x => x);
                var lastLoggedInUser = usersInGroup.OrderByDescending(x => x.LastLoginDate).FirstOrDefault();
                if (lastLoggedInUser?.LastLoginDate == null) continue;
                var local = DateTime.SpecifyKind(lastLoggedInUser.LastLoginDate, DateTimeKind.Local);
                yield return new AppTelemetry<DateTime>($"Umbraco.Users.LastLoginDate.{group.Key}", local.ToUniversalTime());
            }
        }
    }
}