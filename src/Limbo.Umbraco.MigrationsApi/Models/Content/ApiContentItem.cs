using Newtonsoft.Json;
using Skybrud.Essentials.Time;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Extensions;

namespace Limbo.Umbraco.MigrationsApi.Models.Content;

public class ApiContentItem {

    [JsonProperty("id", Order = -999)]
    public int Id { get; }

    [JsonProperty("key", Order = -998)]
    public Guid Key { get; }

    [JsonProperty("name", Order = -997)]
    public string Name { get; }

    [JsonProperty("url", Order = -996)]
    public string Url { get; }

    [JsonProperty("type", Order = -995)]
    public string Type { get; }

    [JsonProperty("sortOrder", Order = -994)]
    public int SortOrder { get; }

    [JsonProperty("createDate", Order = -993)]
    public EssentialsTime CreateDate { get; }

    [JsonProperty("updateDate", Order = -994)]
    public EssentialsTime UpdateDate { get; }

    [JsonProperty("children", Order = -900, NullValueHandling = NullValueHandling.Ignore)]
    public IReadOnlyList<ApiContentItem>? Children { get; }

    public ApiContentItem(IPublishedContent content, IReadOnlyList<ApiContentItem>? children) {
        Id = content.Id;
        Key = content.Key;
        Name = content.Name ?? string.Empty;
        Url = content.Url();
        Type = content.ContentType.Alias;
        SortOrder = content.SortOrder;
        CreateDate = EssentialsTime.FromTicks(content.CreateDate.Ticks, TimeZoneInfo.Local);
        UpdateDate = EssentialsTime.FromTicks(content.UpdateDate.Ticks, TimeZoneInfo.Local);
        Children = children;
    }



}