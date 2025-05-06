# Limbo Migrations API

[![GitHub license](https://img.shields.io/badge/license-MIT-blue.svg)](https://github.com/limbo-works/Limbo.Umbraco.MigrationsApi/blob/v8/main/LICENSE.md)
[![NuGet](https://img.shields.io/nuget/vpre/Limbo.Umbraco.MigrationsApi.svg)](https://www.nuget.org/packages/Limbo.Umbraco.MigrationsApi)
[![NuGet](https://img.shields.io/nuget/dt/Limbo.Umbraco.MigrationsApi.svg)](https://www.nuget.org/packages/Limbo.Umbraco.MigrationsApi)
[![Limbo.Umbraco.MigrationsApi at packages.limbo.works](https://img.shields.io/badge/limbo-packages-blue)](https://packages.limbo.works/limbo.umbraco.migrationsapi/)

Adds an API to Umbraco 8 for exporting content, media and members. The API can then be consumed by [**Limbo.Umbraco.MigrationsClient**](https://github.com/limbo-works/Limbo.Umbraco.MigrationsClient) and [**Limbo.Umbraco.Migrations**](https://github.com/limbo-works/Limbo.Umbraco.Migrations).

<table>
  <tr>
    <td><strong>License:</strong></td>
    <td><a href="https://github.com/limbo-works/Limbo.Umbraco.MigrationsApi/blob/v8/main/LICENSE.md"><strong>MIT License</strong></a></td>
  </tr>
  <tr>
    <td><strong>Umbraco:</strong></td>
    <td>Umbraco 8.6+</td>
  </tr>
  <tr>
    <td><strong>Target Framework:</strong></td>
    <td>.NET Framework 4.7.2</td>
  </tr>
</table>










<br /><br />

## Installation

### Umbraco 8

Via  [**NuGet**](https://www.nuget.org/packages/Limbo.Umbraco.MigrationsApi/8.0.3):

```
dotnet add package Limbo.Umbraco.MigrationsApi --version 8.0.3
```

or:

```
Install-Package Limbo.Umbraco.MigrationsApi -Version 8.0.3
```

### Other versions of Umbraco

- [**`v1/main`**](https://github.com/limbo-works/Limbo.Umbraco.MigrationsApi/tree/v1/main) (Umbraco 7)





<br /><br />

## Configuration

The package can be configured via the `<appSettings>` element in your `Web.config` file.

The endpoints exposed by this package requires an API key. You can set the API key by adding a new setting with the key `LimboMigrationsApiKey` like shown below:

```xml
<add key="LimboMigrationsApiKey" value="your secret key" />
```

The users endpoint is disabled by default. To enable it, you can add the following setting:

```xml
<add key="LimboMigrationsApiUsersEnabled" value="true" />
```

Some (but still not all) endpoints also support an IP allow list, so if one or more IP addresses have been specified, the requesting IP address must be in the list to gain access:

```xml
<add key="LimboMigrationsApiAllowList" value="111.111.111.111,222.222.222.222" />
```



<br /><br />

## Endpoints

### Content

```
GET /umbraco/Limbo/Migrations/GetContentAtRoot?maxLevel={int}
GET /umbraco/Limbo/Migrations/GetContentById?id={int}&maxLevel={int}
GET /umbraco/Limbo/Migrations/GetContentByKey?key={guid}&maxLevel={int}
```

### Content Types

```
GET /umbraco/Limbo/Migrations/GetContentTypes
GET /umbraco/Limbo/Migrations/GetContentTypeById?id={int}
GET /umbraco/Limbo/Migrations/GetContentTypeByKey?key={guid}
GET /umbraco/Limbo/Migrations/GetContentTypeByAlias?alias={string}
```

### Data Types

```
GET /api/limbo/migrations/dataTypes
GET /api/limbo/migrations/dataTypes/{id:int}
GET /api/limbo/migrations/dataTypes/{key:guid}
```

### Grid

```
GET /api/limbo/migrations/grid/editors
```

### Media

```
GET /umbraco/Limbo/Migrations/GetMediaAtRoot
GET /umbraco/Limbo/Migrations/GetMediaById?id={id}&maxLevel={int}
GET /umbraco/Limbo/Migrations/GetMediaByKey?key={guid}&maxLevel={int}
GET /umbraco/Limbo/Migrations/GetMediaTypeByAlias?alias={string}
GET /umbraco/Limbo/Migrations/GetMediaByPath?path={string}&maxLevel={int}
```

### Media Types

```
GET /umbraco/Limbo/Migrations/GetMediaTypes
GET /umbraco/Limbo/Migrations/GetMediaTypeById?id={int}
GET /umbraco/Limbo/Migrations/GetMediaTypeByKey?key={guid}
GET /umbraco/Limbo/Migrations/GetMediaTypeByAlias?alias={string}
```

### Members

```
GET /umbraco/Limbo/Migrations/GetAllMembers
GET /umbraco/Limbo/Migrations/GetMemberById?id={int}
GET /umbraco/Limbo/Migrations/GetMemberByKey?key={key}
```

### Members Types

```
GET /umbraco/Limbo/Migrations/GetMemberTypes
GET /umbraco/Limbo/Migrations/GetMemberTypeById?id={int}
GET /umbraco/Limbo/Migrations/GetMemberTypeByKey?key={guid}
GET /umbraco/Limbo/Migrations/GetMemberTypeByAlias?alias={string}
```

### Users

```
GET /api/limbo/migrations/users
GET /api/limbo/migrations/users/{id:int}
GET /api/limbo/migrations/users/{key:guid}
```