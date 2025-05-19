using Newtonsoft.Json;
using Umbraco.Cms.Core.Models;

namespace Limbo.Umbraco.MigrationsApi.Models.Templates;

public class ApiTemplateItem {

    [JsonProperty("id")]
    public int Id { get; }

    [JsonProperty("key")]
    public Guid Key { get; }

    [JsonProperty("alias")]
    public string Alias { get; }

    [JsonProperty("path")]
    public string Path { get; }

    [JsonProperty("name")]
    public string Name { get; }

    public ApiTemplateItem(ITemplate template) {
        Id = template.Id;
        Key = template.Key;
        Alias = template.Alias;
        Path = template.Path;
        Name = template.Name ?? string.Empty;
    }

}