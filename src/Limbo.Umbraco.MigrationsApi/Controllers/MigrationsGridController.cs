using System.Web.Http;
using Umbraco.Cms.Core.Configuration.Grid;
using Umbraco.Cms.Web.Common.Attributes;

namespace Limbo.Umbraco.MigrationsApi.Controllers;

[PluginController("LimboMigrations")]
public class MigrationsGridController : MigrationsControllerBase {

    [HttpGet]
    [Route("api/limbo/migrations/grid/editors")]
    public object GetEditors() {

        if (!HasAccess(out string reason)) return Unauthorized(reason);

        IGridConfig gridConfig = Current.Configs.Grids();

        return gridConfig.EditorsConfig.Editors;

    }

}
