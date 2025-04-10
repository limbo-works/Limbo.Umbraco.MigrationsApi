using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Skybrud.Essentials.Strings.Extensions;
using Skybrud.Essentials.Time;
using Umbraco.Core.Models;

namespace Limbo.Umbraco.MigrationsApi.Models.DataTypes {

    public class ApiDataType {

        [JsonProperty("id")]
        public int Id { get; }

        [JsonProperty("key")]
        public Guid Key { get; }

        [JsonProperty("name")]
        public string Name { get; }

        [JsonProperty("dbType")]
        public string DbType { get; }

        [JsonProperty("editorAlias")]
        public string EditorAlias { get; }

        [JsonProperty("editor")]
        public object Editor { get; }

        [JsonProperty("createDate")]
        public EssentialsTime CreateDate { get; }

        [JsonProperty("updateDate")]
        public EssentialsTime UpdateDate { get; }

        [JsonProperty("config")]
        public JObject Config { get; }

        public ApiDataType(IDataType dataType) {
            Id = dataType.Id;
            Key = dataType.Key;
            Name = dataType.Name;
            DbType = dataType.DatabaseType.ToKebabCase();
            CreateDate = EssentialsTime.FromTicks(dataType.CreateDate.Ticks, TimeZoneInfo.Local);
            UpdateDate = EssentialsTime.FromTicks(dataType.UpdateDate.Ticks, TimeZoneInfo.Local);
            EditorAlias = dataType.EditorAlias;
            Editor = dataType.Editor is null ? null : new ApiDataEditor(dataType.Editor);
            Config = dataType.Configuration is null ? new JObject() : JObject.FromObject(dataType.Configuration);
        }

    }

}