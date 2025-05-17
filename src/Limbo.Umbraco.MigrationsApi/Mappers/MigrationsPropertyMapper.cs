using Limbo.Umbraco.MigrationsApi.Models.Properties;
using Newtonsoft.Json.Linq;
using Skybrud.Essentials.Collections.Extensions;
using Skybrud.Essentials.Json.Newtonsoft;
using Skybrud.Essentials.Time;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Models.PublishedContent;

namespace Limbo.Umbraco.MigrationsApi.Mappers;

public class MigrationsPropertyMapper {

    public virtual IReadOnlyDictionary<string, ApiProperty> MapProperties(IPublishedContent content) {

        Dictionary<string, ApiProperty> properties = [];

        foreach (IPublishedProperty property in content.Properties) {
            properties.Add(property.Alias, MapProperty(property));
        }

        return properties;

    }

    public virtual ApiProperty MapProperty(IPublishedProperty property) {

        object? propertyValue = property.GetSourceValue();

        if (propertyValue is string strValue) {
            strValue = strValue.Trim();
            if (strValue.StartsWith('{') && strValue.EndsWith('}') && JsonUtils.TryParseJsonObject(strValue, out JObject? objectValue)) {
                propertyValue = objectValue;
            } else if (strValue.StartsWith('[') && strValue.EndsWith(']') && JsonUtils.TryParseJsonArray(strValue, out JArray? arrayValue)) {
                propertyValue = arrayValue;
            }
        } else if (propertyValue is DateTime dateTime) {
            // Adjust for Umbraco/NPoco returning the incorrect kind
            propertyValue = EssentialsTime.FromTicks(dateTime.Ticks, TimeZoneInfo.Local);
        }

        return new ApiProperty(property.Alias, property.PropertyType.EditorAlias, propertyValue);

    }

    public virtual ApiPropertyGroup Map(PropertyGroup propertyGroup) {

        IReadOnlyList<ApiPropertyType> propertyTypes = propertyGroup.PropertyTypes?
            .Cast<PropertyType>().SelectList(Map) ?? [];

        return new ApiPropertyGroup(propertyGroup, propertyTypes);

    }

    public virtual ApiPropertyType Map(PropertyType propertyType) {
        return new ApiPropertyType(propertyType);
    }

    public virtual IReadOnlyDictionary<string, ApiProperty> MapProperties(IMember member) {

        Dictionary<string, ApiProperty> properties = [];

        foreach (IProperty property in member.Properties) {
            properties.Add(property.Alias, MapProperty(property));
        }

        return properties;

    }

    public virtual ApiProperty MapProperty(IProperty property) {

        object? propertyValue = property.GetValue();

        if (propertyValue is string strValue) {
            strValue = strValue.Trim();
            if (strValue.StartsWith("{") && strValue.EndsWith("}") && JsonUtils.TryParseJsonObject(strValue, out JObject? objectValue)) {
                propertyValue = objectValue;
            } else if (strValue.StartsWith("[") && strValue.EndsWith("]") && JsonUtils.TryParseJsonArray(strValue, out JArray? arrayValue)) {
                propertyValue = arrayValue;
            }
        } else if (propertyValue is DateTime dateTime) {
            // Adjust for Umbraco/NPoco returning the incorrect kind
            propertyValue = EssentialsTime.FromTicks(dateTime.Ticks, TimeZoneInfo.Local);
        }

        return new ApiProperty(property.Alias, property.PropertyType.PropertyEditorAlias, propertyValue);

    }

}