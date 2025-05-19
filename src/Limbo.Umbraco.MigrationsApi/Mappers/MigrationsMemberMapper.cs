using Limbo.Umbraco.MigrationsApi.Models.Members;
using Limbo.Umbraco.MigrationsApi.Models.Properties;
using Umbraco.Cms.Core.Models;

namespace Limbo.Umbraco.MigrationsApi.Mappers;

public class MigrationsMemberMapper {

    private readonly MigrationsPropertyMapper _propertyMapper;

    public MigrationsMemberMapper(MigrationsPropertyMapper propertyMapper) {
        _propertyMapper = propertyMapper;
    }

    public virtual ApiMember Map(IMember member) {

        IReadOnlyDictionary<string, ApiProperty> properties = _propertyMapper.MapProperties(member);

        return new ApiMember(member, properties);

    }

}