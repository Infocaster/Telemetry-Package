using System;

namespace Infocaster.Telemetry.Umbraco
{
    /// <summary>
    /// Provides a guid that uniquely identifies the telemetry report source application.
    /// </summary>
    public interface IAppIdentifierProvider
    {
        Guid GetAppId();
    }
}