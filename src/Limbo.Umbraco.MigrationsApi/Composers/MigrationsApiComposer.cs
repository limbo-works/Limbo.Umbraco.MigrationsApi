using Limbo.Umbraco.MigrationsApi.Mappers;
using Limbo.Umbraco.MigrationsApi.Models.Settings;
using Microsoft.Extensions.DependencyInjection;
using Umbraco.Cms.Core.Composing;
using Umbraco.Cms.Core.DependencyInjection;

namespace Limbo.Umbraco.MigrationsApi.Composers;

public class MigrationsApiComposer : IComposer {

    public void Compose(IUmbracoBuilder builder) {

        builder.Services.AddOptions<MigrationsApiSettings>()
            .Bind(builder.Config.GetSection("Limbo:Migrations:Api"))
            .ValidateDataAnnotations();

        builder.Services.AddSingleton<MigrationsContentMapper>();
        builder.Services.AddSingleton<MigrationsContentTypeMapper>();
        builder.Services.AddSingleton<MigrationsMediaMapper>();
        builder.Services.AddSingleton<MigrationsMediaTypeMapper>();
        builder.Services.AddSingleton<MigrationsMemberMapper>();
        builder.Services.AddSingleton<MigrationsMemberTypeMapper>();
        builder.Services.AddSingleton<MigrationsPropertyMapper>();
        builder.Services.AddSingleton<MigrationsTemplateMapper>();

    }

}