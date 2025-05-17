using Limbo.Umbraco.MigrationsApi.Mappers;
using Limbo.Umbraco.MigrationsApi.Models.Settings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Web.Common.Attributes;

namespace Limbo.Umbraco.MigrationsApi.Controllers;

[PluginController("LimboMigrations")]
public class MigrationsMembersController : MigrationsControllerBase {

    private readonly IMemberService _memberService;
    private readonly MigrationsMemberMapper _mapper;

    public MigrationsMembersController(IOptions<MigrationsApiSettings> options, IMemberService memberService, MigrationsMemberMapper mapper) : base(options) {
        _memberService = memberService;
        _mapper = mapper;
    }

    #region Public API methods

    [HttpGet]
    [Route("api/limbo/migrations/members")]
    [Route("umbraco/Limbo/Migrations/GetAllMembers")]
    public object GetMembers() {
        if (!HasAccess(out string reason)) return Unauthorized(reason);
        return _memberService.GetAllMembers().Select(_mapper.Map);
    }

    [HttpGet]
    [Route("api/limbo/migrations/members/{id:int}")]
    [Route("umbraco/Limbo/Migrations/GetMemberById")]
    public object GetContentTypeById(int id) {
        if (!HasAccess(out string reason)) return Unauthorized(reason);
        IMember? member = _memberService.GetById(id);
        return member == null ? NotFound("Member not found.") : _mapper.Map(member);
    }

    [HttpGet]
    [Route("api/limbo/migrations/members/{key:guid}")]
    [Route("umbraco/Limbo/Migrations/GetMemberByKey")]
    public object GetContentTypeByKey(Guid key) {
        if (!HasAccess(out string reason)) return Unauthorized(reason);
        IMember? member = _memberService.GetByKey(key);
        return member == null ? NotFound("Member not found.") : _mapper.Map(member);
    }

    #endregion

    protected override bool HasAccess(out string rejectionReason) {
        if (!base.HasAccess(out rejectionReason)) return false;
        if (Settings.Members.Enabled) return true;
        rejectionReason = "Members controller has not been enabled.";
        return false;
    }

}