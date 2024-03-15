---
order: 2
---

# Authentication

The migrations API uses basic authentication, where the username is `api` and the password is your configured API key.

## cURL

A CURL request to get all root content could look like this:

```
curl --location --request GET 'https://your-old-site.com/umbraco/Limbo/Migrations/GetContentAtRoot' \
--header 'Authorization: Basic YXBpOnlvdXIgcGFzc3dvcmQ='
```

The `YXBpOnlvdXIgcGFzc3dvcmQ=` value is the combined Base64 hash of `api` and `your password`.

## C#

Using our [**Limbo.Umbraco.MigrationsClient**](https://packages.limbo.works/limbo.umbraco.migrationsclient/) package, a similar call could be made in C# like as shown below:

```cshtml
@using Limbo.Umbraco.MigrationsClient
@using Limbo.Umbraco.MigrationsClient.Models.Content
@using Limbo.Umbraco.MigrationsClient.Responses
@using Skybrud.Essentials.Http.Exceptions
@inherits Microsoft.AspNetCore.Mvc.Razor.RazorPage

@{

    // Initialize the HTTP client
    MigrationsHttpClient client = new MigrationsHttpClient("https", "your-old-site.com", "Your API key");

    try {

        // Make the request to the migrations API
        IMigrationsResponse<IReadOnlyList<LegacyContentItem>> response = client.GetContentAtRoot();

        <p>Found @response.Body.Count pages at root</p>

        // Iterate through the root content
        foreach (ILegacyContentItem content in response.Body) {

            <hr />

            // Write out some basic information about the content node
            <pre>ID: @content.Id</pre>
            <pre>Key: @content.Key</pre>
            <pre>Name: @content.Name</pre>

        }

    } catch (HttpException ex) {

        <pre>@ex</pre>

    }

}
```