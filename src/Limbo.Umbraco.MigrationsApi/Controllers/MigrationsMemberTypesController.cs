using Limbo.Umbraco.MigrationsApi.Mappers;
using Limbo.Umbraco.MigrationsApi.Models.Settings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Web.Common.Attributes;

namespace Limbo.Umbraco.MigrationsApi.Controllers;

[PluginController("LimboMigrations")]
public class MigrationsMemberTypesController : MigrationsControllerBase {

    private readonly IMemberTypeService _memberTypeService;
    private readonly MigrationsMemberTypeMapper _mapper;

    public MigrationsMemberTypesController(IOptions<MigrationsApiSettings> options, IMemberTypeService memberTypeService, MigrationsMemberTypeMapper mapper) : base(options) {
        _memberTypeService = memberTypeService;
        _mapper = mapper;
    }

    [HttpGet]
    [Route("api/limbo/migrations/memberTypes")]
    [Route("umbraco/Limbo/Migrations/GetMemberTypes")]
    public object GetMemberTypes() {
        if (!HasAccess(out string reason)) return Unauthorized(reason);
        return _memberTypeService.GetAll().Select(x => _mapper.Map(x));
    }

    [HttpGet]
    [Route("api/limbo/migrations/memberTypes/{id:int}")]
    [Route("umbraco/Limbo/Migrations/GetMemberTypeById")]
    public object GetMemberTypeById(int id) {
        if (!HasAccess(out string reason)) return Unauthorized(reason);
        IMemberType? membersType = _memberTypeService.Get(id);
        return membersType is null ? NotFound("Member type not found.") : _mapper.Map(membersType);
    }

    [HttpGet]
    [Route("api/limbo/migrations/memberTypes/{key:guid}")]
    [Route("umbraco/Limbo/Migrations/GetMemberTypeByKey")]
    public object GetMemberTypeByKey(Guid key) {
        if (!HasAccess(out string reason)) return Unauthorized(reason);
        IMemberType? membersType = _memberTypeService.Get(key);
        return membersType is null ? NotFound("Member type not found.") : _mapper.Map(membersType);
    }

    [HttpGet]
    [Route("api/limbo/migrations/memberTypes/{alias}")]
    [Route("umbraco/Limbo/Migrations/GetMemberTypeByAlias")]
    public object GetMemberTypeByAlias(string alias) {
        if (!HasAccess(out string reason)) return Unauthorized(reason);
        IMemberType? membersType = _memberTypeService.Get(alias);
        return membersType is null ? NotFound("Member type not found.") : _mapper.Map(membersType);
    }

}