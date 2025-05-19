using System.Diagnostics.CodeAnalysis;
using Limbo.Umbraco.MigrationsApi.Models.Templates;
using Umbraco.Cms.Core.Models;

namespace Limbo.Umbraco.MigrationsApi.Mappers;

public class MigrationsTemplateMapper {

    [return: NotNullIfNotNull(nameof(template))]
    public virtual ApiTemplateItem? MapItem(ITemplate? template) {
        return template is null ? null : new ApiTemplateItem(template);
    }

}