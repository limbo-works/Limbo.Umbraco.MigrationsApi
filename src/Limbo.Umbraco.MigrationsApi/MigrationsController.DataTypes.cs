using System;
using System.Linq;
using System.Web.Http;
using Limbo.Umbraco.MigrationsApi.Models.DataTypes;
using Umbraco.Core.Models;

namespace Limbo.Umbraco.MigrationsApi;

public partial class MigrationsController {

    [HttpGet]
    public object GetDataTypeById(int id) {
        if (!HasAccess()) return Unauthorized();
        IDataType dataType = _dataTypeService.GetDataType(id);
        return MapDataType(dataType);
    }

    [HttpGet]
    public object GetDataTypeByKey(Guid key) {
        if (!HasAccess()) return Unauthorized();
        IDataType dataType = _dataTypeService.GetDataType(key);
        return MapDataType(dataType);
    }

    [HttpGet]
    public object GetDataTypes() {
        if (!HasAccess()) return Unauthorized();
        return _dataTypeService
            .GetAll()
            .Select(MapDataType);
    }

    private static object MapDataType(IDataType dataType) {
        return dataType == null ? null : new ApiDataType(dataType);
    }

}
