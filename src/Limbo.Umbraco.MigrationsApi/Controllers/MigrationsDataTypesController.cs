using Limbo.Umbraco.MigrationsApi.Models.DataTypes;
using Limbo.Umbraco.MigrationsApi.Models.Settings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Web.Common.Attributes;

namespace Limbo.Umbraco.MigrationsApi.Controllers;

[PluginController("LimboMigrations")]
public class MigrationsDataTypesController : MigrationsControllerBase {

    private readonly IDataTypeService _dataTypeService;

    public MigrationsDataTypesController(IOptions<MigrationsApiSettings>  options, IDataTypeService dataTypeService) : base(options) {
        _dataTypeService = dataTypeService;
    }

    [HttpGet]
    [Route("api/limbo/migrations/dataTypes")]
    public object GetDataTypes() {
        if (!HasAccess(out string reason)) return Unauthorized(reason);
        return _dataTypeService
            .GetAll()
            .Select(x => new ApiDataType(x));
    }

    [HttpGet]
    [Route("api/limbo/migrations/dataTypes/{id:int}")]
    public object GetDataTypeById(int id) {
        if (!HasAccess(out string reason)) return Unauthorized(reason);
        IDataType? dataType = _dataTypeService.GetDataType(id);
        return dataType is null ? NotFound() : new ApiDataType(dataType);
    }

    [HttpGet]
    [Route("api/limbo/migrations/dataTypes/{key:guid}")]
    public object GetDataTypeByKey(Guid key) {
        if (!HasAccess(out string reason)) return Unauthorized(reason);
        IDataType? dataType = _dataTypeService.GetDataType(key);
        return dataType is null ? NotFound() : new ApiDataType(dataType);
    }

}
