<h3 align="center">
<img height="100" src="https://raw.githubusercontent.com/Infocaster/.github/main/assets/infocaster_nuget_yellow.svg">
</h3>

<h1 align="center">
Infocaster Telemetry Package

[![Downloads](https://img.shields.io/nuget/dt/Infocaster.Telemetry.Umbraco?color=ff0069)](https://www.nuget.org/packages/Infocaster.Telemetry.Umbraco/)
[![Nuget](https://img.shields.io/nuget/vpre/Infocaster.Telemetry.Umbraco?color=ffc800)](https://www.nuget.org/packages/Infocaster.Telemetry.Umbraco/)
![GitHub](https://img.shields.io/github/license/Infocaster/Telemetry-Package?color=ff0069)
</h1>

*Awesome telemetry package for Umbraco by Infocaster. Do you want to keep track of your Umbraco websites? Then this package may be for you!*

This package makes keeping track of your Umbraco websites easier! When installed your websites will periodically send telemetry reports to a centralized and external web API. The reports include data about your Umbraco website like which versions of Umbraco and Examine are installed, how many log messages were created, when your content was last updated and much, much more!
Centralizing your data allows you to easily spot which websites require attention and removes the need to check in on your websites manually.

The data is sent to an API of your choosing. This ensures that you maintain full control over your data and that your data is never shared with other parties. See the section about requirements for more information about the API.

The reporting is fully customizable. You can add or remove data or customize the reporting to fit your needs. See the section about customization for more information.

## Example data
The JSON structure below is an example of the data that is included in telemetry reports. All data is collected at runtime.

```json
{
    "AppId": "xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx",
    "AppName": "myawesomeumbracowebsite.com",
    "Telemetry": [{
            "Name": "Azure.WebsiteDisableOverlappedRecycling",
            "Value": "1",
            "Type": "System.String"
        }, {
            "Name": "Umbraco.Examine.ExternalIndexItemCount",
            "Value": 8592,
            "Type": "System.Int32"
        }, {
            "Name": "Umbraco.Examine.InternalIndexItemCount",
            "Value": 9493,
            "Type": "System.Int32"
        }, {
            "Name": "Umbraco.Examine.MembersIndexItemCount",
            "Value": 0,
            "Type": "System.Int32"
        }, {
            "Name": "Umbraco.Examine.LuceneDirectoryFactory",
            "Value": "Examine.LuceneEngine.Directories.SyncTempEnvDirectoryFactory, Examine",
            "Type": "System.String"
        }, {
            "Name": "Umbraco.Examine.ExamineVersion",
            "Value": "3.0.0.0",
            "Type": "System.String"
        }, {
            "Name": "Umbraco.ApplicationUrl",
            "Value": "https://myawesomeumbracowebsite.com/umbraco",
            "Type": "System.String"
        }, {
            "Name": "Umbraco.ApplicationUrlSetting",
            "Value": "https://myawesomeumbracowebsite.com/umbraco",
            "Type": "System.String"
        }, {
            "Name": "Umbraco.Content.LastUpdatedDate",
            "Value": "2022-10-17T18:46:19.4817556Z",
            "Type": "System.DateTime"
        }, {
            "Name": "Umbraco.DebugMode",
            "Value": false,
            "Type": "System.Boolean"
        }, {
            "Name": "Umbraco.Domains.en-US",
            "Value": "myawesomeumbracowebsite.com",
            "Type": "System.String"
        }, {
            "Name": "Umbraco.Domains.nl-NL",
            "Value": "myawesomeumbracowebsite.nl",
            "Type": "System.String"
        }, {
            "Name": "Umbraco.Domains.Count",
            "Value": 2,
            "Type": "System.Int32"
        }, {
            "Name": "Umbraco.LocalTempPath",
            "Value": "D:\local\Temp\UmbracoData\3cee2dd43eaf1d6130dfa7f7671f7721",
            "Type": "System.String"
        }, {
            "Name": "Umbraco.LocalTempStorageLocation",
            "Value": "EnvironmentTemp",
            "Type": "System.String"
        }, {
            "Name": "Umbraco.Logs.Debug.Count",
            "Value": 0,
            "Type": "System.Int32"
        }, {
            "Name": "Umbraco.Logs.Information.Count",
            "Value": 206,
            "Type": "System.Int32"
        }, {
            "Name": "Umbraco.Logs.Warning.Count",
            "Value": 36,
            "Type": "System.Int32"
        }, {
            "Name": "Umbraco.Logs.Error.Count",
            "Value": 50,
            "Type": "System.Int32"
        }, {
            "Name": "Umbraco.Logs.Fatal.Count",
            "Value": 0,
            "Type": "System.Int32"
        }, {
            "Name": "Umbraco.Logs.LogLevel",
            "Value": "Information",
            "Type": "System.String"
        }, {
            "Name": "Umbraco.IsMainDom",
            "Value": true,
            "Type": "System.Boolean"
        }, {
            "Name": "Umbraco.MainDomLockSetting",
            "Value": "SqlMainDomLock",
            "Type": "System.String"
        }, {
            "Name": "Umbraco.SessionTimeOutInMinutes",
            "Value": 20,
            "Type": "System.Int32"
        }, {
            "Name": "Umbraco.UseHttps",
            "Value": false,
            "Type": "System.Boolean"
        }, {
            "Name": "Umbraco.Users.LastLoginDate.Administrators",
            "Value": "2022-10-19T06:27:39.4534862Z",
            "Type": "System.DateTime"
        }, {
            "Name": "Umbraco.Users.LastLoginDate.Sensitive data",
            "Value": "2022-10-19T06:27:39.4534862Z",
            "Type": "System.DateTime"
        }, {
            "Name": "Umbraco.VersionCheckPeriod",
            "Value": 7,
            "Type": "System.Int32"
        }, {
            "Name": "Umbraco.UmbracoVersion",
            "Value": "10.0.0",
            "Type": "System.String"
        }, {
            "Name": "System.TargetFramework",
            "Value": ".NETCoreApp,Version=v6.0",
            "Type": "System.String"
        }
    ]
}
```

## Requirements
The package requires a web API to send telemetry to and a database to store telemetry in. The API and the database will need to be hosted and maintained by you. Feel free to use our example source code for your API. The source code is available at https://github.com/Infocaster/Telemetry-Backend. The source code includes a Blazor app to visualize your data too!

## Installation
Make sure your web API is up and running first. See our example API source code at https://github.com/Infocaster/Telemetry-Backend for more information.

Then install the Infocaster.Telemetry.Umbraco package in your website. The package is available via NuGet. Visit [the package on NuGet](https://www.nuget.org/packages/Infocaster.Telemetry.Umbraco/) for more information about installing the package using NuGet.

Umbraco version 7.6 and later are supported. Make sure to install the right package version for your website. See the table below for which package version is compatible with your website.

| Umbraco version | Package version |
|----------------:|:----------------|
|           < 7.6 | unsupported     |
|           > 7.6 | > 7             |
|             > 8 | > 8             |
|             > 9 | > 9             |
|            > 10 | > 10            |

The packages requires configuration after installation. See the section about configuration for more information.

## Configuration
The package has several configurable properties that can be changed in appsettings.json. The properties in the JSON structure below are required to enable reporting. See the table below for a complete overview of the configurable properties.

```json
{
    "Telemetry": {
        "ApiEndpoint": "https://example.com/api/telemetry",
        "EnableReporting": true,
        "AppId": "xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx",
        "AppName": "myawesomeumbracowebsite.com"
    }
}
```
|               Name | Type   | Required | Default  | Description |
|-------------------:|:-------|:---------|:---------|:------------|
|        ApiEndpoint | string | Yes      |          | Endpoint url of your web API to send telemetry reports to, for example: "https://example.com/api/telemetry".
|  ApiAuthHeaderName | string | No       |          | Name of the HTTP request header for authorizing with your web API, for example: "Authorization". Leave empty if your web API requires no authorization.
| ApiAuthHeaderValue | string | No       |          | Value of the HTTP request header for authorizing with your web API, for example: "Basic XXXXXXXX". Leave empty if your web API requires no authorization.
|  DelayMilliseconds | int    | No       | 60000    | Delay in milliseconds after app start for sending the initial telemetry report. Default is 1 minute after app start.
| PeriodMilliseconds | int    | No       | 86400000 | Period in milliseconds between sending telemetry reports. Default is every 24 hours.
|    EnableReporting | bool   | Yes      | false    | Setting indicating if telemetry reporting is enabled.
|              AppId | guid   | Yes      |          | Guid that uniquely identifies your Umbraco website. Use your favorite guid generator to create a new guid.
|            AppName | string | Yes      |          | Preferred display name of your Umbraco website in telemetry reports, for example: "myawesomeumbracowebsite.com".

## Customization
The reporting of data is customizable using a code-first approach.

To add data you can implement ITelemetryProvider:

```csharp
public class MyTelemetryProvider : ITelemetryProvider
{
    public IEnumerable<IAppTelemetry> GetTelemetry()
    {
        yield return new AppTelemetry<string>("My.Custom.Telemetry", "My website is awesome!");
    }
}
```

You can then use a composer to register your telemetry provider:

```csharp
public class Composer : IComposer
{
    public void Compose(IUmbracoBuilder builder)
    {
        builder.Services.AddTransient<ITelemetryProvider, MyTelemetryProvider>();
    }
}
```

You can use a composer to remove the default telemetry providers:

```csharp
public class Composer : IComposer
{
    public void Compose(IUmbracoBuilder builder)
    {
        // Remove all telemetry providers
        builder.Services.RemoveAll<ITelemetryProvider>();
        
        // Remove a specific telemetry provider
        var serviceDescriptor = builder.Services.First(s => s.ImplementationType == typeof(UmbracoVersionProvider));
        builder.Services.Remove(serviceDescriptor);
    }
}
```

You can add custom reporting by implementing ITelemetryReporter:

```csharp
public class MyTelemetryReporter : ITelemetryReporter
{
    public virtual async Task ReportTelemetry(AppTelemetryReport report)
    {
        // Send the report via HTTP request or SMTP or whichever way that suits your needs!
    }
}
```

You can then use a composer to register your telemetry reporter:

```csharp
public class Composer : IComposer
{
    public void Compose(IUmbracoBuilder builder)
    {
        builder.Services.AddTransient<ITelemetryReporter, MyTelemetryReporter>();
    }
}
```

You may opt to remove the default telemetry reporter if you implement custom reporting. You can use a composer to do this:

```csharp
public class Composer : IComposer
{
    public void Compose(IUmbracoBuilder builder)
    {
        builder.Services.RemoveAll<ITelemetryReporter>();
    }
}
```

Finally you can opt to extend the default telemetry reporter and to override the default implementation:

```csharp
public class MyTelemetryReporter : TelemetryReporter
{
    public MyTelemetryReporter(
        IOptions<TelemetryReportingConfiguration> configuration,
        IHttpClientFactory httpClientFactory,
        ILogger<MyTelemetryReporter> logger)
            : base(configuration, httpClientFactory, logger)
    {

    }

    public override void SetRequestAuthHeader(HttpRequestMessage request)
    {
        request.Headers.Authorization = new AuthenticationHeaderValue("Apikey", "MySuperSecretApikey");
    }
}
```

You can then use a composer to replace the default telemetry reporter with your telemetry reporter:

```csharp
public class Composer : IComposer
{
    public void Compose(IUmbracoBuilder builder)
    {
        builder.Services.Replace(ServiceDescriptor.Transient(typeof(ITelemetryReporter), typeof(MyTelemetryReporter)));
    }
}
```
## Contributing
This package is open for contributions. If you want to contribute to the source code, please check out our [guide to contributing](/docs/CONTRIBUTING.md).  

<a href="https://infocaster.net">
<img align="right" height="200" src="https://raw.githubusercontent.com/Infocaster/.github/main/assets/Infocaster_Corner.png?raw=true">
</a>