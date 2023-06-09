using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Configuration;
using System.Web.Http;
using Newtonsoft.Json.Linq;
using Skybrud.Essentials.Json.Newtonsoft;
using Skybrud.Essentials.Security;
using Skybrud.Essentials.Strings;
using Skybrud.Essentials.Strings.Extensions;
using Skybrud.Essentials.Time;
using Skybrud.WebApi.Json;
using Umbraco.Core.Models;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Web;
using Umbraco.Web.Mvc;
using Umbraco.Web.WebApi;

namespace Limbo.Umbraco.MigrationsApi {

    [JsonOnlyConfiguration]
    [PluginController("Limbo")]
    public class MigrationsController : UmbracoApiController {

        public int? MaxLevel = HttpContext.Current.Request.QueryString["maxLevel"].ToInt32OrNull();

        [HttpGet]
        public object GetContentAtRoot() {
            if (!HasAccess()) return Unauthorized();
            return Umbraco.TypedContentAtRoot().Select(x => MapContentItem(x, MaxLevel));
        }

        [HttpGet]
        public object GetContentById(int id) {
            if (!HasAccess()) return Unauthorized();
            IPublishedContent content = Umbraco.TypedContent(id);
            return content == null ? NotFound() : MapContent(content, MaxLevel);
        }

        [HttpGet]
        public object GetContentByKey(Guid key) {
            if (!HasAccess()) return Unauthorized();
            IPublishedContent content = Umbraco.TypedContent(key);
            return content == null ? NotFound() : MapContent(content, MaxLevel);
        }

        [HttpGet]
        public object GetMediaAtRoot() {
            if (!HasAccess()) return Unauthorized();
            return Umbraco.TypedMediaAtRoot().Select(x => MapMediaItem(x, MaxLevel));
        }

        [HttpGet]
        public object GetMediaById(int id) {
            if (!HasAccess()) return Unauthorized();
            IPublishedContent media = Umbraco.TypedMedia(id);
            return media == null ? NotFound() : MapMedia(media, MaxLevel);
        }

        [HttpGet]
        public object GetMediaByKey(Guid key) {
            if (!HasAccess()) return Unauthorized();
            IPublishedContent media = Umbraco.TypedMedia(key);
            return media == null ? NotFound() : MapMedia(media, MaxLevel);
        }

        [HttpGet]
        public object GetMemberById(int id) {
            if (!HasAccess()) return Unauthorized();
            IMember member = ApplicationContext.Services.MemberService.GetById(id);
            return member == null ? NotFound() : MapMember(member);
        }

        [HttpGet]
        public object GetMemberByKey(Guid key) {
            if (!HasAccess()) return Unauthorized();
            IMember member = ApplicationContext.Services.MemberService.GetByKey(key);
            return member == null ? NotFound() : MapMember(member);
        }

        [HttpGet]
        public object GetAllMembers() {
            if (!HasAccess()) return Unauthorized();
            return ApplicationContext.Services.MemberService.GetAllMembers().Select(MapMember);
        }

        private static bool HasAccess() {

            string expectedApiKey = WebConfigurationManager.AppSettings["LimboMigrationsApiKey"];
            if (string.IsNullOrWhiteSpace(expectedApiKey)) return false;

            string auth = HttpContext.Current.Request.Headers["Authorization"];
            if (!RegexUtils.IsMatch(auth ?? string.Empty, "Basic (.+?)$", out Match m)) return false;

            try {
                return SecurityUtils.Base64Decode(m.Groups[1].Value) == $"api:{expectedApiKey}";
            } catch {
                return false;
            }

        }

        private object MapMediaItem(IPublishedContent media, int? maxLevel = null) {

            if (maxLevel == null) maxLevel = media.Level + 1;

            JObject json = JObject.FromObject(new {
                id = media.Id,
                key = media.GetKey(),
                name = media.Name,
                url = media.Url,
                type = media.DocumentTypeAlias,
                createDate = new EssentialsTime(media.CreateDate, TimeZoneInfo.Local),
                updateDate = new EssentialsTime(media.UpdateDate, TimeZoneInfo.Local)
            });

            if (media.Level < maxLevel) {
                List<object> children = new List<object>();
                foreach (var child in media.Children) {
                    children.Add(MapMediaItem(child, maxLevel));
                }
                json.Add("children", JToken.FromObject(children));
            }

            return json;

        }

        private object MapContentItem(IPublishedContent content, int? maxLevel = null) {

            if (maxLevel == null) maxLevel = content.Level + 1;

            JObject json = JObject.FromObject(new {
                id = content.Id,
                key = content.GetKey(),
                name = content.Name,
                url = content.Url,
                type = content.DocumentTypeAlias,
                createDate = new EssentialsTime(content.CreateDate, TimeZoneInfo.Local),
                updateDate = new EssentialsTime(content.UpdateDate, TimeZoneInfo.Local)
            });

            if (content.Level < maxLevel) {
                List<object> children = new List<object>();
                foreach (var child in content.Children) {
                    children.Add(MapContentItem(child, maxLevel));
                }
                json.Add("children", JToken.FromObject(children));
            }

            return json;

        }

        protected virtual object MapMedia(IPublishedContent media, int? maxLevel = null) {
            JObject json = JObject.FromObject(MapMediaItem(media, maxLevel));

            json["path"] = JToken.FromObject(GetPath(media, x => MapMediaItem(x, maxLevel)));
            json["properties"] = MapProperties(media);

            return json;

        }

        protected virtual object MapMember(IMember member) {

            JObject json = JObject.FromObject(new {
                id = member.Id,
                key = member.Key,
                name = member.Name,
                type = member.ContentTypeAlias,
                createDate = new EssentialsTime(member.CreateDate, TimeZoneInfo.Local),
                updateDate = new EssentialsTime(member.UpdateDate, TimeZoneInfo.Local)
            });

            json["properties"] = MapProperties(member);

            return json;

        }

        private object MapContent(IPublishedContent content, int? maxLevel = null) {

            if (maxLevel == null) maxLevel = content.Level + 1;
            JObject json = JObject.FromObject(MapContentItem(content, maxLevel));

            json["path"] = JToken.FromObject(GetPath(content, y => MapContentItem(y, 0)));
            json["properties"] = MapProperties(content);

            return json;

        }

        private JToken MapProperty(IPublishedProperty property) {

            object propertyValue = property.DataValue;

            if (propertyValue is string strValue) {
                strValue = strValue.Trim();
                if (strValue.StartsWith("{") && strValue.EndsWith("}") && JsonUtils.TryParseJsonObject(strValue, out JObject objectValue)) {
                    propertyValue = objectValue;
                } else if (strValue.StartsWith("[") && strValue.EndsWith("]") && JsonUtils.TryParseJsonArray(strValue, out JArray arrayValue)) {
                    propertyValue = arrayValue;
                }
            } else if (propertyValue is DateTime dateTime) {
                propertyValue = new EssentialsTime(dateTime, TimeZoneInfo.Local);
            }

            var propertyType = (PublishedPropertyType) property
                .GetType()
                .GetField("PropertyType", BindingFlags.Public | BindingFlags.Instance)?
                .GetValue(property);

            return JToken.FromObject(new {
                alias = property.PropertyTypeAlias,
                editorAlias = propertyType?.PropertyEditorAlias,
                value = propertyValue
            });

        }

        private JToken MapProperty(Property property) {

            object propertyValue = property.Value;

            if (propertyValue is string strValue) {
                strValue = strValue.Trim();
                if (strValue.StartsWith("{") && strValue.EndsWith("}") && JsonUtils.TryParseJsonObject(strValue, out JObject objectValue)) {
                    propertyValue = objectValue;
                } else if (strValue.StartsWith("[") && strValue.EndsWith("]") && JsonUtils.TryParseJsonArray(strValue, out JArray arrayValue)) {
                    propertyValue = arrayValue;
                }
            } else if (propertyValue is DateTime dateTime) {
                propertyValue = new EssentialsTime(dateTime, TimeZoneInfo.Local);
            }

            return JToken.FromObject(new {
                alias = property.Alias,
                editorAlias = property.PropertyType.Alias,
                value = propertyValue
            });

        }

        private JToken MapProperties(IPublishedContent content) {

            JObject properties = new JObject();

            foreach (IPublishedProperty property in content.Properties) {
                properties.Add(property.PropertyTypeAlias, MapProperty(property));
            }

            return properties;

        }

        private JToken MapProperties(IMember member) {

            JObject properties = new JObject();

            foreach (Property property in member.Properties) {
                properties.Add(property.Alias, MapProperty(property));
            }

            return properties;

        }

        private List<T> GetPath<T>(IPublishedContent content, Func<IPublishedContent, T> func) {

            List<T> path = new List<T>();

            IPublishedContent parent = content.Parent;

            while (parent != null) {

                path.Add(func(parent));

                parent = parent.Parent;

            }

            path.Reverse();

            return path;

        }

    }

}