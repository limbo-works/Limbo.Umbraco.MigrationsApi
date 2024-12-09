using System.Linq;
using System;
using System.Web.Http;
using Newtonsoft.Json.Linq;
using Umbraco.Core.Models;
using Skybrud.Essentials.Time;

namespace Limbo.Umbraco.MigrationsApi {

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
            if (dataType == null) return null;
            return new {
                id = dataType.Id,
                key = dataType.Key,
                name = dataType.Name,
                dbType = dataType.DatabaseType,
                createDate = EssentialsTime.FromTicks(dataType.CreateDate.Ticks, TimeZoneInfo.Local),
                updateDate = EssentialsTime.FromTicks(dataType.UpdateDate.Ticks, TimeZoneInfo.Local),
                editorAlias = dataType.EditorAlias,
                editor = MapDataEditor(dataType),
                config = dataType.Configuration is null ? new JObject() : JObject.FromObject(dataType.Configuration)
            };
        }

        private static object MapDataEditor(IDataType dataType) {

            if (dataType.Editor is null) return null;

            return new {
                alias = dataType.Editor.Alias,
                name = dataType.Editor.Name,
                icon = dataType.Editor.Icon,
                group = dataType.Editor.Group,
                type = dataType.Editor.Type.ToString(),
                deprecated = dataType.Editor.IsDeprecated
            };

        }

    }

}