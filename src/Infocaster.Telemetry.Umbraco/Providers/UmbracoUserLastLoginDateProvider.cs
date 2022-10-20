using System;
using System.Collections.Generic;
using System.Linq;
using Umbraco.Cms.Core.Services;

namespace Infocaster.Telemetry.Umbraco.Providers
{
    public class UmbracoUserLastLoginDateProvider : ITelemetryProvider
    {
        private readonly IUserService _userService;

        public UmbracoUserLastLoginDateProvider(IUserService userService)
        {
            _userService = userService;
        }

        public IEnumerable<IAppTelemetry> GetTelemetry()
        {
            var userGroups = _userService.GetAllUserGroups().ToList();
            foreach (var userGroup in userGroups)
            {
                var users = _userService.GetAllInGroup(userGroup.Id);
                var lastLoggedInUser = users.OrderByDescending(x => x.LastLoginDate).FirstOrDefault();
                if (lastLoggedInUser?.LastLoginDate is null) continue;
                var local = DateTime.SpecifyKind(lastLoggedInUser.LastLoginDate.Value, DateTimeKind.Local);
                yield return new AppTelemetry<DateTime>($"Umbraco.Users.LastLoginDate.{userGroup.Name}", local.ToUniversalTime());
            }
        }
    }
}