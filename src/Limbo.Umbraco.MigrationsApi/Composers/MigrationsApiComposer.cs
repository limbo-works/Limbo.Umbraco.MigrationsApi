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

    }

}