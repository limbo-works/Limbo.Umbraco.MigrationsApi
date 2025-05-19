using Limbo.Umbraco.MigrationsApi.Mappers;
using Limbo.Umbraco.MigrationsApi.Models.Settings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Skybrud.Essentials.Strings.Extensions;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Web.Common.Attributes;

namespace Limbo.Umbraco.MigrationsApi.Controllers;

[PluginController("LimboMigrations")]
public class MigrationsMediaController : MigrationsControllerBase {

    private readonly IMediaService _mediaService;
    private readonly IUmbracoContextAccessor _umbracoContextAccessor;
    private readonly MigrationsMediaMapper _mapper;

    #region Properties

    private int? MaxLevel => Request.Query["maxLevel"].ToString().ToInt32OrNull();

    #endregion

    public MigrationsMediaController(IOptions<MigrationsApiSettings> options, IMediaService mediaService, IUmbracoContextAccessor umbracoContextAccessor, MigrationsMediaMapper mapper) : base(options) {
        _mediaService = mediaService;
        _umbracoContextAccessor = umbracoContextAccessor;
        _mapper = mapper;
    }

    #region Public API methods

    [HttpGet]
    [Route("api/limbo/migrations/media/root")]
    [Route("umbraco/Limbo/Migrations/GetMediaAtRoot")]
    public object GetContentTypes() {
        if (!HasAccess(out string reason)) return Unauthorized(reason);
        if (!_umbracoContextAccessor.TryGetUmbracoContext(out IUmbracoContext? umbracoContext)) return Problem("Could not obtain Umbraco Context");
        return umbracoContext.Media?.GetAtRoot().Select(x => _mapper.MapItem(x, MaxLevel)) ?? [];
    }

    [HttpGet]
    [Route("api/limbo/migrations/media/{id:int}")]
    [Route("umbraco/Limbo/Migrations/GetMediaById")]
    public object GetContentTypeById(int id) {
        if (!HasAccess(out string reason)) return Unauthorized(reason);
        if (!_umbracoContextAccessor.TryGetUmbracoContext(out IUmbracoContext? umbracoContext)) return Problem("Could not obtain Umbraco Context");
        IPublishedContent? content = umbracoContext.Media?.GetById(id);
        return content is null ? NotFound("Media not found.") : _mapper.Map(content, MaxLevel);
    }

    [HttpGet]
    [Route("api/limbo/migrations/media/{key:guid}")]
    [Route("umbraco/Limbo/Migrations/GetMediaByKey")]
    public object GetContentTypeByKey(Guid key) {
        if (!HasAccess(out string reason)) return Unauthorized(reason);
        if (!_umbracoContextAccessor.TryGetUmbracoContext(out IUmbracoContext? umbracoContext)) return Problem("Could not obtain Umbraco Context");
        IPublishedContent? content = umbracoContext.Media?.GetById(key);
        return content is null ? NotFound("Media not found.") : _mapper.Map(content, MaxLevel);
    }

    [HttpGet]
    [Route("api/limbo/migrations/media")]
    [Route("umbraco/Limbo/Migrations/GetMediaByPath")]
    public object GetContentTypeByPath(string path) {
        if (!HasAccess(out string reason)) return Unauthorized(reason);
        if (!_umbracoContextAccessor.TryGetUmbracoContext(out IUmbracoContext? umbracoContext)) return Problem("Could not obtain Umbraco Context");
        IMedia? media = _mediaService.GetMediaByPath(path);
        IPublishedContent? content = media is null ? null : umbracoContext.Media?.GetById(media.Id);
        return content is null ? NotFound("Media not found.") : _mapper.Map(content, MaxLevel);

    }

    #endregion

}