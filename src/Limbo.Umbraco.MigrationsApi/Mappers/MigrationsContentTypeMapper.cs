using Limbo.Umbraco.MigrationsApi.Models.ContentTypes;
using Limbo.Umbraco.MigrationsApi.Models.Properties;
using Limbo.Umbraco.MigrationsApi.Models.Templates;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Services;

namespace Limbo.Umbraco.MigrationsApi.Mappers;

public class MigrationsContentTypeMapper {

    public IContentTypeService ContentTypeService { get; }

    public MigrationsTemplateMapper TemplateMapper { get; }

    public MigrationsPropertyMapper PropertyMapper { get; }

    public MigrationsContentTypeMapper(IContentTypeService contentTypeService, MigrationsTemplateMapper templateMapper, MigrationsPropertyMapper propertyMapper) {
        ContentTypeService = contentTypeService;
        TemplateMapper = templateMapper;
        PropertyMapper = propertyMapper;
    }

    public virtual ApiContentType Map(IContentType contentType) {

        ApiTemplateItem? defaultTemplate = TemplateMapper.MapItem(contentType.DefaultTemplate);

        List<ApiContentTypeItem> allowedContentTypes = [];
        foreach (ContentTypeSort hej in contentType.AllowedContentTypes ?? []) {
            if (ContentTypeService.Get(hej.Alias) is { } full) allowedContentTypes.Add(MapItem(full));
        }

        List<ApiTemplateItem> allowedTemplates = [];
        foreach (ITemplate template in contentType.AllowedTemplates ?? []) {
            allowedTemplates.Add(TemplateMapper.MapItem(template));
        }

        List<ApiContentTypeItem> compositions = [];
        foreach (int hej in contentType.CompositionIds()) {
            if (ContentTypeService.Get(hej) is { } composition) compositions.Add(MapItem(composition));
        }

        List<ApiPropertyGroup> propertyGroups = [];
        foreach (PropertyGroup propertyGroup in contentType.PropertyGroups) {
            propertyGroups.Add(PropertyMapper.Map(propertyGroup));
        }

        return new ApiContentType(contentType, defaultTemplate, allowedContentTypes, allowedTemplates, compositions, propertyGroups);

    }

    public virtual ApiContentTypeItem MapItem(IContentType contentType) {
        return new ApiContentTypeItem(contentType);
    }

}