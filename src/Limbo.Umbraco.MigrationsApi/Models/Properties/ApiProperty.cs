using Newtonsoft.Json;

namespace Limbo.Umbraco.MigrationsApi.Models.Properties;

public class ApiProperty {

    [JsonProperty("alias")]
    public string Alias { get; }

    [JsonProperty("editorAlias")]
    public string EditorAlias { get; }

    [JsonProperty("value")]
    public object? Value { get; }

    public ApiProperty(string alias, string editorAlias, object? value) {
        Alias = alias;
        EditorAlias = editorAlias;
        Value = value;
    }

}