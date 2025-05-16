using Limbo.Umbraco.MigrationsApi.Models.DataTypes;
using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Core.Models;

namespace Limbo.Umbraco.MigrationsApi;

public partial class MigrationsController {

    [HttpGet]
    public IActionResult GetDataTypeById(int id) {
        if (!HasAccess()) return Unauthorized("Access Denied.");
        IDataType dataType = _dataTypeService.GetDataType(id);
        return Ok(MapDataType(dataType));
    }

    [HttpGet]
    public IActionResult GetDataTypeByKey(Guid key) {
        if (!HasAccess()) return Unauthorized("Access Denied.");
        IDataType dataType = _dataTypeService.GetDataType(key);
        return Ok(MapDataType(dataType));
    }

    [HttpGet]
    public IActionResult GetDataTypes() {
        if (!HasAccess()) return Unauthorized("Access Denied.");
        return Ok(_dataTypeService
            .GetAll()
            .Select(MapDataType));
    }

    private static object MapDataType(IDataType dataType) {
        return dataType == null ? null : new ApiDataType(dataType);
    }

}
