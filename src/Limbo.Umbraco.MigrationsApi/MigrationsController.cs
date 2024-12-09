using System;
using System.Collections.Generic;
using System.Linq;
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
using Umbraco.Core.Composing;
using Umbraco.Core.Models;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Core.Services;
using Umbraco.Web.Mvc;
using Umbraco.Web.WebApi;

namespace Limbo.Umbraco.MigrationsApi {

    [JsonOnlyConfiguration]
    [PluginController("Limbo")]
    public partial class MigrationsController : UmbracoApiController {

        private readonly IContentTypeService _contentTypeService;
        private readonly IDataTypeService _dataTypeService;
        private readonly IMediaTypeService _mediaTypeService;
        private readonly IMemberTypeService _memberTypeService;
        private readonly IMemberService _memberService;

        public int? MaxLevel = HttpContext.Current.Request.QueryString["maxLevel"].ToInt32OrNull();

        #region Constructors

        public MigrationsController(IContentTypeService contentTypeService, IDataTypeService dataTypeService, IMediaTypeService mediaTypeService, IMemberTypeService memberTypeService, IMemberService memberService) {
            _contentTypeService = contentTypeService;
            _dataTypeService = dataTypeService;
            _mediaTypeService = mediaTypeService;
            _memberTypeService = memberTypeService;
            _memberService = memberService;
        }

        #endregion

        #region Public API methods

        [HttpGet]
        public object GetContentAtRoot() {
            if (!HasAccess()) return Unauthorized();
            return Umbraco.ContentAtRoot().Select(x => MapContentItem(x, MaxLevel));
        }

        [HttpGet]
        public object GetContentById(int id) {
            if (!HasAccess()) return Unauthorized();
            IPublishedContent content = Umbraco.Content(id);
            return content == null ? NotFound() : MapContent(content, MaxLevel);
        }

        [HttpGet]
        public object GetContentByKey(Guid key) {
            if (!HasAccess()) return Unauthorized();
            IPublishedContent content = Umbraco.Content(key);
            return content == null ? NotFound() : MapContent(content, MaxLevel);
        }

        [HttpGet]
        public object GetMediaAtRoot() {
            if (!HasAccess()) return Unauthorized();
            return Umbraco.MediaAtRoot().Select(x => MapMediaItem(x, MaxLevel));
        }

        [HttpGet]
        public object GetMediaById(int id) {
            if (!HasAccess()) return Unauthorized();
            IPublishedContent media = Umbraco.Media(id);
            return media == null ? NotFound() : MapMedia(media, MaxLevel);
        }

        [HttpGet]
        public object GetMediaByKey(Guid key) {
            if (!HasAccess()) return Unauthorized();
            IPublishedContent media = Umbraco.Media(key);
            return media == null ? NotFound() : MapMedia(media, MaxLevel);
        }

        [HttpGet]
        public object GetMediaByPath(string path) {
            if (!HasAccess()) return Unauthorized();
            IMedia media = Current.Services.MediaService.GetMediaByPath(path);
            IPublishedContent published = media == null ? null : Umbraco.Media(media.Key);
            return media == null ? NotFound() : MapMedia(published, MaxLevel);
        }

        [HttpGet]
        public object GetMemberById(int id) {
            if (!HasAccess()) return Unauthorized();
            IMember member = _memberService.GetById(id);
            return member == null ? NotFound() : MapMember(member);
        }

        [HttpGet]
        public object GetMemberByKey(Guid key) {
            if (!HasAccess()) return Unauthorized();
            IMember member = _memberService.GetByKey(key);
            return member == null ? NotFound() : MapMember(member);
        }

        [HttpGet]
        public object GetAllMembers() {
            if (!HasAccess()) return Unauthorized();
            return _memberService.GetAllMembers().Select(MapMember);
        }

        [HttpGet]
        public object GetContentTypeById(int id) {
            if (!HasAccess()) return Unauthorized();
            IContentType contentType = _contentTypeService.Get(id);
            return MapContentType(contentType);
        }

        [HttpGet]
        public object GetContentTypeByKey(Guid key) {
            if (!HasAccess()) return Unauthorized();
            IContentType contentType = _contentTypeService.Get(key);
            return MapContentType(contentType);
        }

        [HttpGet]
        public object GetContentTypeByAlias(string alias) {
            if (!HasAccess()) return Unauthorized();
            IContentType contentType = _contentTypeService.Get(alias);
            return MapContentType(contentType);
        }

        [HttpGet]
        public object GetContentTypes() {
            if (!HasAccess()) return Unauthorized();
            return _contentTypeService
                .GetAll()
                .Select(MapContentType);
        }

        [HttpGet]
        public object GetMediaTypeById(int id) {
            if (!HasAccess()) return Unauthorized();
            IMediaType contentType = _mediaTypeService.Get(id);
            return MapMediaType(contentType);
        }

        [HttpGet]
        public object GetMediaTypeByKey(Guid key) {
            if (!HasAccess()) return Unauthorized();
            IMediaType contentType = _mediaTypeService.Get(key);
            return MapMediaType(contentType);
        }

        [HttpGet]
        public object GetMediaTypeByAlias(string alias) {
            if (!HasAccess()) return Unauthorized();
            IMediaType contentType = _mediaTypeService.Get(alias);
            return MapMediaType(contentType);
        }

        [HttpGet]
        public object GetMediaTypes() {
            if (!HasAccess()) return Unauthorized();
            return _mediaTypeService
                .GetAll()
                .Select(MapMediaType);
        }

        [HttpGet]
        public object GetMemberTypeById(int id) {
            if (!HasAccess()) return Unauthorized();
            IMemberType memberType = _memberTypeService.Get(id);
            return MapMemberType(memberType);
        }

        [HttpGet]
        public object GetMemberTypeByKey(Guid key) {
            if (!HasAccess()) return Unauthorized();
            IMemberType memberType = _memberTypeService.Get(key);
            return MapMemberType(memberType);
        }

        [HttpGet]
        public object GetMemberTypeByAlias(string alias) {
            if (!HasAccess()) return Unauthorized();
            IMemberType memberType = _memberTypeService.Get(alias);
            return MapMemberType(memberType);
        }

        [HttpGet]
        public object GetMemberTypes() {
            if (!HasAccess()) return Unauthorized();
            return _memberTypeService
                .GetAll()
                .Select(MapMemberType);
        }

        #endregion

        private object MapContentType(IContentType contentType) {
            if (contentType == null) return null;
            return new {
                id = contentType.Id,
                key = contentType.Key,
                alias = contentType.Alias,
                name = contentType.Name,
                icon = contentType.Icon,
                tabs = contentType.CompositionPropertyGroups.Select(MapPropertyGroup),
                defaultTemplate = MapTemplateItem(contentType.DefaultTemplate),
                allowedAsRoot = contentType.AllowedAsRoot,
                allowedContentTypes = contentType.AllowedContentTypes.Select(MapContentTypeItem),
                allowedTemplate = contentType.AllowedTemplates.Select(MapTemplateItem),
                compositions = contentType
                    .CompositionIds()
                    .Select(x => _contentTypeService.Get(x))
                    .Select(MapContentTypeItem)
            };
        }

        private object MapContentTypeItem(ContentTypeSort contentType) {
            return MapContentTypeItem(_contentTypeService.Get(contentType.Alias));
        }

        private object MapContentTypeItem(IContentType contentType) {
            return contentType == null ? null : new { id = contentType.Id, key = contentType.Key, alias = contentType.Alias, name = contentType.Name };
        }

        private object MapTemplateItem(ITemplate template) {
            return template == null ? null : new { id = template.Id, key = template.Key, alias = template.Alias, name = template.Name };
        }

        private object MapMediaType(IMediaType mediaType) {
            if (mediaType == null) return null;
            return new {
                id = mediaType.Id,
                key = mediaType.Key,
                alias = mediaType.Alias,
                name = mediaType.Name,
                icon = mediaType.Icon,
                tabs = mediaType.CompositionPropertyGroups.Select(MapPropertyGroup),
                allowedAsRoot = mediaType.AllowedAsRoot,
                allowedContentTypes = mediaType.AllowedContentTypes.Select(MapMediaTypeItem),
                compositions = mediaType
                    .CompositionIds()
                    .Select(x => _mediaTypeService.Get(x))
                    .Select(MapMediaTypeItem)
            };
        }

        private object MapMediaTypeItem(ContentTypeSort mediaType) {
            return MapMediaTypeItem(_mediaTypeService.Get(mediaType.Alias));
        }

        private object MapMediaTypeItem(IMediaType mediaType) {
            return mediaType == null ? null : new { id = mediaType.Id, key = mediaType.Key, alias = mediaType.Alias, name = mediaType.Name };
        }

        private object MapMemberType(IMemberType memberType) {
            if (memberType == null) return null;
            return new {
                id = memberType.Id,
                key = memberType.Key,
                alias = memberType.Alias,
                name = memberType.Name,
                icon = memberType.Icon,
                tabs = memberType.CompositionPropertyGroups.Select(MapPropertyGroup),
                compositions = memberType
                    .CompositionIds()
                    .Select(x => _memberTypeService.Get(x))
                    .Select(MapMemberTypeItem)
            };
        }

        private object MapMemberTypeItem(IMemberType memberType) {
            return memberType == null ? null : new { id = memberType.Id, key = memberType.Key, alias = memberType.Alias, name = memberType.Name };
        }

        private object MapPropertyGroup(PropertyGroup propertyGroup) {
            return new {
                id = propertyGroup.Id,
                key = propertyGroup.Key,
                name = propertyGroup.Name,
                sortOrder = propertyGroup.SortOrder,
                properties = propertyGroup.PropertyTypes.Select(MapPropertyType)
            };
        }

        private object MapPropertyType(PropertyType propertyType) {
            return new {
                id = propertyType.Id,
                key = propertyType.Key,
                alias = propertyType.Alias,
                name = propertyType.Name,
                description = propertyType.Description,
                sortOrder = propertyType.SortOrder,
                editorAlias = propertyType.PropertyEditorAlias,
                dataTypeId = propertyType.DataTypeId,
                mandatory = propertyType.Mandatory
            };
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
                key = media.Key,
                name = media.Name,
                url = media.Url,
                type = media.ContentType.Alias,
                createDate = EssentialsTime.FromTicks(media.CreateDate.Ticks, TimeZoneInfo.Local),
                updateDate = EssentialsTime.FromTicks(media.UpdateDate.Ticks, TimeZoneInfo.Local)
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
                key = content.Key,
                name = content.Name,
                url = content.Url,
                type = content.ContentType.Alias,
                sortOrder = content.SortOrder,
                createDate = EssentialsTime.FromTicks(content.CreateDate.Ticks, TimeZoneInfo.Local),
                updateDate = EssentialsTime.FromTicks(content.UpdateDate.Ticks, TimeZoneInfo.Local)
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
                createDate = EssentialsTime.FromTicks(member.CreateDate.Ticks, TimeZoneInfo.Local),
                updateDate = EssentialsTime.FromTicks(member.UpdateDate.Ticks, TimeZoneInfo.Local)
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

            object propertyValue = property.GetSourceValue();

            if (propertyValue is string strValue) {
                strValue = strValue.Trim();
                if (strValue.StartsWith("{") && strValue.EndsWith("}") && JsonUtils.TryParseJsonObject(strValue, out JObject objectValue)) {
                    propertyValue = objectValue;
                } else if (strValue.StartsWith("[") && strValue.EndsWith("]") && JsonUtils.TryParseJsonArray(strValue, out JArray arrayValue)) {
                    propertyValue = arrayValue;
                }
            } else if (propertyValue is DateTime dateTime) {
                // Adjust for Umbraco/NPoco returning the incorrect kind
                propertyValue = EssentialsTime.FromTicks(dateTime.Ticks, TimeZoneInfo.Local);
            }

            return JToken.FromObject(new {
                alias = property.Alias,
                editorAlias = property.PropertyType.EditorAlias,
                value = propertyValue
            });

        }

        private JToken MapProperty(Property property) {

            object propertyValue = property.GetValue();

            if (propertyValue is string strValue) {
                strValue = strValue.Trim();
                if (strValue.StartsWith("{") && strValue.EndsWith("}") && JsonUtils.TryParseJsonObject(strValue, out JObject objectValue)) {
                    propertyValue = objectValue;
                } else if (strValue.StartsWith("[") && strValue.EndsWith("]") && JsonUtils.TryParseJsonArray(strValue, out JArray arrayValue)) {
                    propertyValue = arrayValue;
                }
            } else if (propertyValue is DateTime dateTime) {
                // Adjust for Umbraco/NPoco returning the incorrect kind
                propertyValue = EssentialsTime.FromTicks(dateTime.Ticks, TimeZoneInfo.Local);
            }

            return JToken.FromObject(new {
                alias = property.Alias,
                editorAlias = property.PropertyType.PropertyEditorAlias,
                value = propertyValue
            });

        }

        private JToken MapProperties(IPublishedContent content) {

            JObject properties = new JObject();

            foreach (IPublishedProperty property in content.Properties) {
                properties.Add(property.Alias, MapProperty(property));
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