using Limbo.Umbraco.MigrationsApi.Mappers;
using Limbo.Umbraco.MigrationsApi.Models.Settings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Skybrud.Essentials.Strings.Extensions;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Web.Common.Attributes;

namespace Limbo.Umbraco.MigrationsApi.Controllers;

[PluginController("LimboMigrations")]
public class MigrationsContentController : MigrationsControllerBase {

    private readonly IUmbracoContextAccessor _umbracoContextAccessor;
    private readonly MigrationsContentMapper _mapper;

    #region Properties

    private int? MaxLevel => Request.Query["maxLevel"].ToString().ToInt32OrNull();

    #endregion

    public MigrationsContentController(IOptions<MigrationsApiSettings> options, IUmbracoContextAccessor umbracoContextAccessor, MigrationsContentMapper mapper) : base(options) {
        _umbracoContextAccessor = umbracoContextAccessor;
        _mapper = mapper;
    }

    #region Public API methods

    [HttpGet]
    [Route("api/limbo/migrations/content/root")]
    [Route("umbraco/Limbo/Migrations/GetContentAtRoot")]
    public object GetContentTypes() {
        if (!HasAccess(out string reason)) return Unauthorized(reason);
        if (!_umbracoContextAccessor.TryGetUmbracoContext(out IUmbracoContext? umbracoContext)) return Problem("Could not obtain Umbraco Context");
        return umbracoContext.Content?.GetAtRoot().Select(x => _mapper.MapItem(x, MaxLevel)) ?? [];
    }

    [HttpGet]
    [Route("api/limbo/migrations/content/{id:int}")]
    [Route("umbraco/Limbo/Migrations/GetContentById")]
    public object GetContentTypeById(int id) {
        if (!HasAccess(out string reason)) return Unauthorized(reason);
        if (!_umbracoContextAccessor.TryGetUmbracoContext(out IUmbracoContext? umbracoContext)) return Problem("Could not obtain Umbraco Context");
        IPublishedContent? content = umbracoContext.Content?.GetById(id);
        return content is null ? NotFound("Content not found.") : _mapper.Map(content, MaxLevel);
    }

    [HttpGet]
    [Route("api/limbo/migrations/content/{key:guid}")]
    [Route("umbraco/Limbo/Migrations/GetContentByKey")]
    public object GetContentTypeByKey(Guid key) {
        if (!HasAccess(out string reason)) return Unauthorized(reason);
        if (!_umbracoContextAccessor.TryGetUmbracoContext(out IUmbracoContext? umbracoContext)) return Problem("Could not obtain Umbraco Context");
        IPublishedContent? content = umbracoContext.Content?.GetById(key);
        return content is null ? NotFound("Content not found.") : _mapper.Map(content, MaxLevel);
    }

    #endregion

}