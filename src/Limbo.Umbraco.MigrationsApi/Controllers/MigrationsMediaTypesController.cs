using Limbo.Umbraco.MigrationsApi.Mappers;
using Limbo.Umbraco.MigrationsApi.Models.Settings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Web.Common.Attributes;

namespace Limbo.Umbraco.MigrationsApi.Controllers;

[PluginController("LimboMigrations")]
public class MigrationsMediaTypesController : MigrationsControllerBase {

    private readonly IMediaTypeService _mediaTypeService;
    private readonly MigrationsMediaTypeMapper _mapper;

    public MigrationsMediaTypesController(IOptions<MigrationsApiSettings>  options, IMediaTypeService mediaTypeService, MigrationsMediaTypeMapper mapper) : base(options) {
        _mediaTypeService = mediaTypeService;
        _mapper = mapper;
    }

    [HttpGet]
    [Route("api/limbo/migrations/mediaTypes")]
    [Route("umbraco/Limbo/Migrations/GetMediaTypes")]
    public object GetMediaTypes() {
        if (!HasAccess(out string reason)) return Unauthorized(reason);
        return _mediaTypeService
            .GetAll()
            .Select(x => _mapper.Map(x));
    }

    [HttpGet]
    [Route("api/limbo/migrations/mediaTypes/{id:int}")]
    [Route("umbraco/Limbo/Migrations/GetMediaTypeById")]
    public object GetMediaTypeById(int id) {
        if (!HasAccess(out string reason)) return Unauthorized(reason);
        IMediaType? mediaType = _mediaTypeService.Get(id);
        return mediaType is null ? NotFound("Media type not found.") : _mapper.Map(mediaType);
    }

    [HttpGet]
    [Route("api/limbo/migrations/mediaTypes/{key:guid}")]
    [Route("umbraco/Limbo/Migrations/GetMediaTypeByKey")]
    public object GetMediaTypeByKey(Guid key) {
        if (!HasAccess(out string reason)) return Unauthorized(reason);
        IMediaType? mediaType = _mediaTypeService.Get(key);
        return mediaType is null ? NotFound("Media type not found.") : _mapper.Map(mediaType);
    }

    [HttpGet]
    [Route("api/limbo/migrations/mediaTypes/{alias}")]
    [Route("umbraco/Limbo/Migrations/GetMediaTypeByAlias")]
    public object GetMediaTypeByAlias(string alias) {
        if (!HasAccess(out string reason)) return Unauthorized(reason);
        IMediaType? mediaType = _mediaTypeService.Get(alias);
        return mediaType is null ? NotFound("Media type not found.") : _mapper.Map(mediaType);
    }

}