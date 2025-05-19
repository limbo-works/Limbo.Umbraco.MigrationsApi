using Limbo.Umbraco.MigrationsApi.Models.Properties;
using Newtonsoft.Json;
using Skybrud.Essentials.Time;
using Umbraco.Cms.Core.Models;

namespace Limbo.Umbraco.MigrationsApi.Models.Members;

public class ApiMember {

    [JsonProperty("id")]
    public int Id { get; }

    [JsonProperty("key")]
    public Guid Key { get; }

    [JsonProperty("name")]
    public string Name { get; }

    [JsonProperty("type")]
    public string Type { get; }

    [JsonProperty("createDate")]
    public EssentialsTime CreateDate { get; }

    [JsonProperty("updateDate")]
    public EssentialsTime UpdateDate { get; }

    [JsonProperty("properties")]
    public IReadOnlyDictionary<string, ApiProperty> Properties { get; }

    public ApiMember(IMember member, IReadOnlyDictionary<string, ApiProperty> properties) {
        Id = member.Id;
        Key = member.Key;
        Name = member.Name ?? string.Empty;
        Type = member.ContentTypeAlias;
        CreateDate = EssentialsTime.FromTicks(member.CreateDate.Ticks, TimeZoneInfo.Local);
        UpdateDate = EssentialsTime.FromTicks(member.UpdateDate.Ticks, TimeZoneInfo.Local);
        Properties = properties;
    }

}