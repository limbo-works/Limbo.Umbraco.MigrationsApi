using Newtonsoft.Json;
using Umbraco.Cms.Core.PropertyEditors;

namespace Limbo.Umbraco.MigrationsApi.Models.DataTypes;

public class ApiDataEditor {

    [JsonProperty("alias")]
    public string Alias { get; }

    [JsonProperty("name")]
    public string Name { get; }

    [JsonProperty("icon")]
    public string Icon { get; }

    [JsonProperty("group")]
    public string Group { get; }

    [JsonProperty("type")]
    public string Type { get; }

    [JsonProperty("deprecated")]
    public bool IsDeprecated { get; }

    public ApiDataEditor(IDataEditor editor) {
        Alias = editor.Alias;
        Name = editor.Name;
        Icon = editor.Icon;
        Group = editor.Group;
        Type = editor.Type.ToString();
        IsDeprecated = editor.IsDeprecated;
    }

}
