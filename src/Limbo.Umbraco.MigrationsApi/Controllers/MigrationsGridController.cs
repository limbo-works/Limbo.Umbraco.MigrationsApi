using Limbo.Umbraco.MigrationsApi.Models.Settings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Umbraco.Cms.Core.Configuration.Grid;

namespace Limbo.Umbraco.MigrationsApi.Controllers;

public class MigrationsGridController : MigrationsControllerBase {

    private readonly IGridConfig _gridConfig;

    public MigrationsGridController(IOptions<MigrationsApiSettings> options, IGridConfig gridConfig) : base(options) {
        _gridConfig = gridConfig;
    }

    [HttpGet]
    [Route("api/limbo/migrations/grid/editors")]
    public object GetEditors() {
        if (!HasAccess(out string reason)) return Unauthorized(reason);
        return _gridConfig.EditorsConfig.Editors;
    }

}