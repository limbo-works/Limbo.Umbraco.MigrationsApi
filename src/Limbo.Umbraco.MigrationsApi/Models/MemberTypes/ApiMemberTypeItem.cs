using Newtonsoft.Json;
using Skybrud.Essentials.Time;
using Umbraco.Cms.Core.Models;

namespace Limbo.Umbraco.MigrationsApi.Models.MemberTypes;

public class ApiMemberTypeItem {

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

    [JsonProperty("createDate", Order = -993)]
    public EssentialsTime CreateDate { get; }

    [JsonProperty("updateDate", Order = -992)]
    public EssentialsTime UpdateDate { get; }

    public ApiMemberTypeItem(IMemberType memberType) {
        Id = memberType.Id;
        Key = memberType.Key;
        Alias = memberType.Alias;
        Path = memberType.Path;
        Name = memberType.Name ?? string.Empty;
        Icon = memberType.Icon ?? string.Empty;
        CreateDate = EssentialsTime.FromTicks(memberType.CreateDate.Ticks, TimeZoneInfo.Local);
        UpdateDate = EssentialsTime.FromTicks(memberType.UpdateDate.Ticks, TimeZoneInfo.Local);
    }

}