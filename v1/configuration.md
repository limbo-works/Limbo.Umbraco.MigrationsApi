# Configuration

In order to access the migrations API you need to add an API key to your `Web.config` file. This can be done by adding `LimboMigrationsApiKey` to the `<appSettings>` element:

```xml
<add key="LimboMigrationsApiKey" value="your secret key" />
```

The API key should be sent along with all requests to the migrations API. Read more about [**authentication**](./authentication.md).

The package currently doesn't any other configuration options.