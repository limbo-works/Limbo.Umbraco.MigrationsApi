using Newtonsoft.Json;
using Umbraco.Cms.Core.Models;

namespace Limbo.Umbraco.MigrationsApi.Models.Properties;

public class ApiPropertyGroup {

    [JsonProperty("id")]
    public int Id { get; }

    [JsonProperty("key")]
    public Guid Key { get; }

    [JsonProperty("name")]
    public string Name { get; }

    [JsonProperty("sortOrder")]
    public int SortOrder { get; }

    [JsonProperty("propertyTypes")]
    public IReadOnlyList<ApiPropertyType> PropertyTypes { get; }

    [JsonProperty("properties")]
    [Obsolete("Use the 'PropertyTypes' property instead.")]
    public IReadOnlyList<ApiPropertyType> Properties => PropertyTypes;

    public ApiPropertyGroup(PropertyGroup propertyGroup, IReadOnlyList<ApiPropertyType> propertyTypes) {
        Id = propertyGroup.Id;
        Key = propertyGroup.Key;
        Name = propertyGroup.Name ?? string.Empty;
        SortOrder = propertyGroup.SortOrder;
        PropertyTypes = propertyTypes;
    }

}