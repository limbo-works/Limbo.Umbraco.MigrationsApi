using Limbo.Umbraco.MigrationsApi.Mappers;
using Limbo.Umbraco.MigrationsApi.Models.Settings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Web.Common.Attributes;

namespace Limbo.Umbraco.MigrationsApi.Controllers;

[PluginController("LimboMigrations")]
public class MigrationsContentTypesController : MigrationsControllerBase {

    private readonly IContentTypeService _contentTypeService;
    private readonly MigrationsContentTypeMapper _mapper;

    public MigrationsContentTypesController(IOptions<MigrationsApiSettings>  options, IContentTypeService contentTypeService, MigrationsContentTypeMapper mapper) : base(options) {
        _contentTypeService = contentTypeService;
        _mapper = mapper;
    }

    [HttpGet]
    [Route("api/limbo/migrations/contentTypes")]
    [Route("umbraco/Limbo/Migrations/GetContentTypes")]
    public object GetContentTypes() {
        if (!HasAccess(out string reason)) return Unauthorized(reason);
        return _contentTypeService
            .GetAll()
            .Select(x => _mapper.Map(x));
    }

    [HttpGet]
    [Route("api/limbo/migrations/contentTypes/{id:int}")]
    [Route("umbraco/Limbo/Migrations/GetContentTypeById")]
    public object GetContentTypeById(int id) {
        if (!HasAccess(out string reason)) return Unauthorized(reason);
        IContentType? contentType = _contentTypeService.Get(id);
        return contentType is null ? NotFound("Content type not found.") : _mapper.Map(contentType);
    }

    [HttpGet]
    [Route("api/limbo/migrations/contentTypes/{key:guid}")]
    [Route("umbraco/Limbo/Migrations/GetContentTypeByKey")]
    public object GetContentTypeByKey(Guid key) {
        if (!HasAccess(out string reason)) return Unauthorized(reason);
        IContentType? contentType = _contentTypeService.Get(key);
        return contentType is null ? NotFound("Content type not found.") : _mapper.Map(contentType);
    }

    [HttpGet]
    [Route("api/limbo/migrations/contentTypes/{alias}")]
    [Route("umbraco/Limbo/Migrations/GetContentTypeByAlias")]
    public object GetContentTypeByAlias(string alias) {
        if (!HasAccess(out string reason)) return Unauthorized(reason);
        IContentType? contentType = _contentTypeService.Get(alias);
        return contentType is null ? NotFound("Content type not found.") : _mapper.Map(contentType);
    }

}