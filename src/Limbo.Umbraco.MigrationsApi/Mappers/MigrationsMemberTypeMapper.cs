using Limbo.Umbraco.MigrationsApi.Models.MemberTypes;
using Limbo.Umbraco.MigrationsApi.Models.Properties;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Services;

namespace Limbo.Umbraco.MigrationsApi.Mappers;

public class MigrationsMemberTypeMapper {

    public IMemberTypeService MemberTypeService { get; }

    public MigrationsTemplateMapper TemplateMapper { get; }

    public MigrationsPropertyMapper PropertyMapper { get; }

    public MigrationsMemberTypeMapper(IMemberTypeService memberTypeService, MigrationsTemplateMapper templateMapper, MigrationsPropertyMapper propertyMapper) {
        MemberTypeService = memberTypeService;
        TemplateMapper = templateMapper;
        PropertyMapper = propertyMapper;
    }

    public virtual ApiMemberType Map(IMemberType memberType) {

        List<ApiMemberTypeItem> compositions = [];
        foreach (int hej in memberType.CompositionIds()) {
            if (MemberTypeService.Get(hej) is { } composition) compositions.Add(MapItem(composition));
        }

        List<ApiPropertyGroup> propertyGroups = [];
        foreach (PropertyGroup propertyGroup in memberType.PropertyGroups) {
            propertyGroups.Add(PropertyMapper.Map(propertyGroup));
        }

        return new ApiMemberType(memberType, compositions, propertyGroups);

    }

    public virtual ApiMemberTypeItem MapItem(IMemberType memberType) {
        return new ApiMemberTypeItem(memberType);
    }

}