using Limbo.Umbraco.MigrationsApi.Models.Properties;
using Limbo.Umbraco.MigrationsApi.Models.Templates;
using Newtonsoft.Json;
using Umbraco.Cms.Core.Models;

namespace Limbo.Umbraco.MigrationsApi.Models.ContentTypes;

public class ApiContentType : ApiContentTypeItem {

    [JsonProperty("allowedAsRoot")]
    public bool AllowedAsRoot { get; }

    [JsonProperty("defaultTemplate")]
    public ApiTemplateItem? DefaultTemplate { get; }

    [JsonProperty("allowedContentTypes")]
    public IReadOnlyList<ApiContentTypeItem> AllowedContentTypes { get; }

    [JsonProperty("allowedTemplates")]
    public IReadOnlyList<ApiTemplateItem> AllowedTemplates { get; }

    [JsonProperty("compositions")]
    public IReadOnlyList<ApiContentTypeItem> Compositions { get; }

    [JsonProperty("propertyGroups")]
    public IReadOnlyList<ApiPropertyGroup> PropertyGroups { get; }

    [JsonProperty("tabs")]
    [Obsolete("Use the 'PropertyGroups' property instead.")]
    public IReadOnlyList<ApiPropertyGroup> Tabs => PropertyGroups;

    public ApiContentType(IContentType contentType, ApiTemplateItem? defaultTemplate, IReadOnlyList<ApiContentTypeItem> allowedContentTypes, IReadOnlyList<ApiTemplateItem> allowedTemplates, IReadOnlyList<ApiContentTypeItem> compositions, IReadOnlyList<ApiPropertyGroup> propertyGroups) : base(contentType) {
        AllowedAsRoot = contentType.AllowedAsRoot;
        DefaultTemplate = defaultTemplate;
        AllowedContentTypes = allowedContentTypes;
        AllowedTemplates = allowedTemplates;
        Compositions = compositions;
        PropertyGroups = propertyGroups;
    }

}