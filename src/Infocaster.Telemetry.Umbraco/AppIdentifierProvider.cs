using System;
using System.Configuration;

namespace Infocaster.Telemetry.Umbraco
{
    /// <summary>
    /// Provides guid from app setting to identify this app.
    /// </summary>
    public class AppIdentifierProvider : IAppIdentifierProvider
    {
        private readonly Lazy<Guid> _appId
            = new Lazy<Guid>(() => GetAppIdFromAppSetting());

        private static Guid GetAppIdFromAppSetting()
        {
            var inter = ConfigurationManager.AppSettings["Telemetry:AppId"];
            if (!Guid.TryParse(inter, out var appId)) return Guid.Empty;
            return appId;
        }

        public virtual Guid GetAppId()
        {
            return _appId.Value;
        }
    }
}