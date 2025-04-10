using System.Web.Http;
using Umbraco.Core;
using Umbraco.Core.Composing;
using Umbraco.Core.Configuration.Grid;
using Umbraco.Web.Mvc;

namespace Limbo.Umbraco.MigrationsApi.Controllers {

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

}