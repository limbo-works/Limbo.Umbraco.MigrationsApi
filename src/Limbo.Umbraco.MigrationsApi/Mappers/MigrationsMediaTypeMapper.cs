using Limbo.Umbraco.MigrationsApi.Models.MediaTypes;
using Limbo.Umbraco.MigrationsApi.Models.Properties;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Services;

namespace Limbo.Umbraco.MigrationsApi.Mappers;

public class MigrationsMediaTypeMapper {

    public IMediaTypeService MediaTypeService { get; }

    public MigrationsTemplateMapper TemplateMapper { get; }

    public MigrationsPropertyMapper PropertyMapper { get; }

    public MigrationsMediaTypeMapper(IMediaTypeService contentTypeService, MigrationsTemplateMapper templateMapper, MigrationsPropertyMapper propertyMapper) {
        MediaTypeService = contentTypeService;
        TemplateMapper = templateMapper;
        PropertyMapper = propertyMapper;
    }

    public virtual ApiMediaType Map(IMediaType mediaType) {

        List<ApiMediaTypeItem> allowedMediaTypes = [];
        foreach (ContentTypeSort hej in mediaType.AllowedContentTypes ?? []) {
            if (MediaTypeService.Get(hej.Alias) is { } full) allowedMediaTypes.Add(MapItem(full));
        }

        List<ApiMediaTypeItem> compositions = [];
        foreach (int hej in mediaType.CompositionIds()) {
            if (MediaTypeService.Get(hej) is { } composition) compositions.Add(MapItem(composition));
        }

        List<ApiPropertyGroup> propertyGroups = [];
        foreach (PropertyGroup propertyGroup in mediaType.PropertyGroups) {
            propertyGroups.Add(PropertyMapper.Map(propertyGroup));
        }

        return new ApiMediaType(mediaType, allowedMediaTypes, compositions, propertyGroups);

    }

    public virtual ApiMediaTypeItem MapItem(IMediaType contentType) {
        return new ApiMediaTypeItem(contentType);
    }

}