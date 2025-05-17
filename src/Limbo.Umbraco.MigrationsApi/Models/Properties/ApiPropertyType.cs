using Newtonsoft.Json;
using Skybrud.Essentials.Strings.Extensions;
using Umbraco.Cms.Core.Models;

namespace Limbo.Umbraco.MigrationsApi.Models.Properties;

public class ApiPropertyType {

    [JsonProperty("id")]
    public int Id { get; }

    [JsonProperty("key")]
    public Guid Key { get; }

    [JsonProperty("alias")]
    public string Alias { get; }

    [JsonProperty("name")]
    public string Name { get; }

    [JsonProperty("description")]
    public string? Description { get; }

    [JsonProperty("sortOrder")]
    public int SortOrder { get; }

    [JsonProperty("editorAlias")]
    public string EditorAlias { get; }

    [JsonProperty("dataTypeId")]
    public int DataTypeId { get; }

    [JsonProperty("mandatory")]
    public bool IsMandatory { get; }

    public ApiPropertyType(PropertyType propertyType) {
        Id = propertyType.Id;
        Key = propertyType.Key;
        Alias = propertyType.Alias;
        Name = propertyType.Name;
        Description = propertyType.Description.NullIfWhiteSpace();
        SortOrder = propertyType.SortOrder;
        EditorAlias = propertyType.PropertyEditorAlias;
        DataTypeId = propertyType.DataTypeId;
        IsMandatory = propertyType.Mandatory;
    }

}