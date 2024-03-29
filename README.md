# Limbo Migrations API

[![GitHub license](https://img.shields.io/badge/license-MIT-blue.svg)](https://github.com/limbo-works/Limbo.Umbraco.MigrationsApi/blob/v1/main/LICENSE.md)
[![NuGet](https://img.shields.io/nuget/vpre/Limbo.Umbraco.MigrationsApi.svg)](https://www.nuget.org/packages/Limbo.Umbraco.MigrationsApi)
[![NuGet](https://img.shields.io/nuget/dt/Limbo.Umbraco.MigrationsApi.svg)](https://www.nuget.org/packages/Limbo.Umbraco.MigrationsApi)
[![Limbo.Umbraco.MigrationsApi at packages.limbo.works](https://img.shields.io/badge/limbo-packages-blue)](https://packages.limbo.works/limbo.umbraco.migrationsapi/)

Adds an API to Umbraco 7 for exporting content, media and members. The API can then be consumed by [**Limbo.Umbraco.MigrationsClient**](https://github.com/limbo-works/Limbo.Umbraco.MigrationsClient) and [**Limbo.Umbraco.Migrations**](https://github.com/limbo-works/Limbo.Umbraco.Migrations).

<table>
  <tr>
    <td><strong>License:</strong></td>
    <td><a href="https://github.com/limbo-works/Limbo.Umbraco.MigrationsApi/blob/v1/main/LICENSE.md"><strong>MIT License</strong></a></td>
  </tr>
  <tr>
    <td><strong>Umbraco:</strong></td>
    <td>Umbraco 7.7+</td>
  </tr>
  <tr>
    <td><strong>Target Framework:</strong></td>
    <td>.NET Framework 4.5</td>
  </tr>
</table>










<br /><br />

### Installation

Via  [**NuGet**](https://www.nuget.org/packages/Limbo.Umbraco.MigrationsApi/1.0.2):

```
dotnet add package Limbo.Umbraco.MigrationsApi --version 1.0.2
```

or:

```
Install-Package Limbo.Umbraco.MigrationsApi -Version 1.0.2
```



<br /><br />

### Configuration

Add a `LimboMigrationsApiKey` to `<appSettings>` in your `Web.config` file:

```xml
<add key="LimboMigrationsApiKey" value="your secret key" />
```
