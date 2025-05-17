using Newtonsoft.Json;
using Skybrud.Essentials.Time;
using Umbraco.Cms.Core.Models;

namespace Limbo.Umbraco.MigrationsApi.Models.MediaTypes;

public class ApiMediaTypeItem {

    [JsonProperty("id", Order = -999)]
    public int Id { get; }

    [JsonProperty("key", Order = -998)]
    public Guid Key { get; }

    [JsonProperty("alias", Order = -997)]
    public string Alias { get; }

    [JsonProperty("path", Order = -996)]
    public string Path { get; }

    [JsonProperty("name", Order = -995)]
    public string Name { get; }

    [JsonProperty("icon", Order = -994)]
    public string Icon { get; }

    [JsonProperty("element", Order = -993)]
    public bool IsElement { get; }

    [JsonProperty("createDate", Order = -992)]
    public EssentialsTime CreateDate { get; }

    [JsonProperty("updateDate", Order = -991)]
    public EssentialsTime UpdateDate { get; }

    public ApiMediaTypeItem(IMediaType mediaType) {
        Id = mediaType.Id;
        Key = mediaType.Key;
        Alias = mediaType.Alias;
        Path = mediaType.Path;
        Name = mediaType.Name ?? string.Empty;
        Icon = mediaType.Icon ?? string.Empty;
        CreateDate = EssentialsTime.FromTicks(mediaType.CreateDate.Ticks, TimeZoneInfo.Local);
        UpdateDate = EssentialsTime.FromTicks(mediaType.UpdateDate.Ticks, TimeZoneInfo.Local);
    }

}