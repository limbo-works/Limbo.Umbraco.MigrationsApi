using Limbo.Umbraco.MigrationsApi.Models.Content;
using Limbo.Umbraco.MigrationsApi.Models.Properties;
using Skybrud.Essentials.Collections.Extensions;
using Umbraco.Cms.Core.Models.PublishedContent;

namespace Limbo.Umbraco.MigrationsApi.Mappers;

public class MigrationsContentMapper {

    private readonly MigrationsPropertyMapper _propertyMapper;

    public MigrationsContentMapper(MigrationsPropertyMapper propertyMapper) {
        _propertyMapper = propertyMapper;
    }

    public ApiContent Map(IPublishedContent content, int? maxLevel) {

        maxLevel ??= content.Level + 1;

        IReadOnlyList<ApiContentItem> path = MapPath(content);

        IReadOnlyList<ApiContentItem>? children = MapChildren(content, maxLevel);

        IReadOnlyDictionary<string, ApiProperty> properties = _propertyMapper.MapProperties(content);

        return new ApiContent(content, path, children, properties);

    }

    public virtual ApiContentItem MapItem(IPublishedContent content, int? maxLevel) {

        IReadOnlyList<ApiContentItem>? children = MapChildren(content, maxLevel);

        return new ApiContentItem(content, children);

    }

    protected virtual IReadOnlyList<ApiContentItem> MapPath(IPublishedContent content) {

        List<ApiContentItem> path = [];

        IPublishedContent? parent = content.Parent;

        while (parent is not null) {
            path.Add(MapItem(parent, 0));
            parent = parent.Parent;
        }

        path.Reverse();

        return path;

    }

    protected virtual IReadOnlyList<ApiContentItem>? MapChildren(IPublishedContent content, int? maxLevel) {
        return content.Level >= maxLevel ? null : content.Children!.SelectList(child => MapItem(child, maxLevel));
    }

}