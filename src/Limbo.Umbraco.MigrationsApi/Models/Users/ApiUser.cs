using Newtonsoft.Json;
using Skybrud.Essentials.Strings.Extensions;
using Skybrud.Essentials.Time;
using Umbraco.Cms.Core.Models.Membership;

namespace Limbo.Umbraco.MigrationsApi.Models.Users;

public class ApiUser {

    [JsonProperty("id")]
    public int Id { get; }

    [JsonProperty("key")]
    public Guid Key { get; }

    [JsonProperty("username")]
    public string Username { get; }

    [JsonProperty("email")]
    public string Email { get; }

    [JsonProperty("name")]
    public string Name { get; }

    [JsonProperty("language")]
    public string? Language { get; }

    [JsonProperty("createDate")]
    public EssentialsTime CreateDate { get; }

    [JsonProperty("updateDate")]
    public EssentialsTime UpdateDate { get; }

    [JsonProperty("avatar", NullValueHandling = NullValueHandling.Ignore)]
    public string? Avatar { get; }

    [JsonProperty("state")]
    public string State { get; }

    public ApiUser(IUser user) {
        Id = user.Id;
        Key = user.Key;
        Username = user.Username;
        Email = user.Email;
        Name = user.Name ?? string.Empty;
        Language = user.Language.NullIfWhiteSpace();
        CreateDate = EssentialsTime.FromTicks(user.CreateDate.Ticks, TimeZoneInfo.Local);
        UpdateDate = EssentialsTime.FromTicks(user.UpdateDate.Ticks, TimeZoneInfo.Local);
        Avatar = user.Avatar.NullIfWhiteSpace();
        State = user.UserState.ToKebabCase();
    }

}
