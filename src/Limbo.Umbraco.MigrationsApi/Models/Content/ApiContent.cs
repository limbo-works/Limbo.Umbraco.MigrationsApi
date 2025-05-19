using Limbo.Umbraco.MigrationsApi.Models.Properties;
using Newtonsoft.Json;
using Umbraco.Cms.Core.Models.PublishedContent;

namespace Limbo.Umbraco.MigrationsApi.Models.Content;

public class ApiContent : ApiContentItem {

    [JsonProperty("path", Order = -950)]
    public IReadOnlyList<ApiContentItem> Path { get; }

    [JsonProperty("properties")]
    public IReadOnlyDictionary<string, ApiProperty> Properties { get; }

    public ApiContent(IPublishedContent content, IReadOnlyList<ApiContentItem> path, IReadOnlyList<ApiContentItem>? children, IReadOnlyDictionary<string, ApiProperty> properties) : base(content, children) {
        Path = path;
        Properties = properties;
    }

}