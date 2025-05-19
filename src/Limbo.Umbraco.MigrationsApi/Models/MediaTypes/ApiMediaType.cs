using Limbo.Umbraco.MigrationsApi.Models.Properties;
using Newtonsoft.Json;
using Umbraco.Cms.Core.Models;

namespace Limbo.Umbraco.MigrationsApi.Models.MediaTypes;

public class ApiMediaType : ApiMediaTypeItem {

    [JsonProperty("allowedAsRoot")]
    public bool AllowedAsRoot { get; }

    [JsonProperty("allowedContentTypes")]
    public IReadOnlyList<ApiMediaTypeItem> AllowedContentTypes { get; }

    [JsonProperty("compositions")]
    public IReadOnlyList<ApiMediaTypeItem> Compositions { get; }

    [JsonProperty("propertyGroups")]
    public IReadOnlyList<ApiPropertyGroup> PropertyGroups { get; }

    [JsonProperty("tabs")]
    [Obsolete("Use the 'PropertyGroups' property instead.")]
    public IReadOnlyList<ApiPropertyGroup> Tabs => PropertyGroups;

    public ApiMediaType(IMediaType mediaType, IReadOnlyList<ApiMediaTypeItem> allowedContentTypes, IReadOnlyList<ApiMediaTypeItem> compositions, IReadOnlyList<ApiPropertyGroup> propertyGroups) : base(mediaType) {
        AllowedAsRoot = mediaType.AllowedAsRoot;
        AllowedContentTypes = allowedContentTypes;
        Compositions = compositions;
        PropertyGroups = propertyGroups;
    }

}