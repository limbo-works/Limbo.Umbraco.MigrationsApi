using Limbo.Umbraco.MigrationsApi.Models.Properties;
using Newtonsoft.Json;
using Umbraco.Cms.Core.Models;

namespace Limbo.Umbraco.MigrationsApi.Models.MemberTypes;

public class ApiMemberType : ApiMemberTypeItem {

    [JsonProperty("compositions")]
    public IReadOnlyList<ApiMemberTypeItem> Compositions { get; }

    [JsonProperty("propertyGroups")]
    public IReadOnlyList<ApiPropertyGroup> PropertyGroups { get; }

    [JsonProperty("tabs")]
    [Obsolete("Use the 'PropertyGroups' property instead.")]
    public IReadOnlyList<ApiPropertyGroup> Tabs => PropertyGroups;

    public ApiMemberType(IMemberType memberType, IReadOnlyList<ApiMemberTypeItem> compositions, IReadOnlyList<ApiPropertyGroup> propertyGroups) : base(memberType) {
        Compositions = compositions;
        PropertyGroups = propertyGroups;
    }

}