using Limbo.Umbraco.MigrationsApi.Models.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Skybrud.Essentials.Guids;
using Umbraco.Cms.Core.Models.Membership;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Web.Common.Attributes;

namespace Limbo.Umbraco.MigrationsApi.Controllers;

[PluginController("LimboMigrations")]
public class MigrationsUsersController : MigrationsControllerBase {

    private readonly IUserService _userService;

    public MigrationsUsersController(IUserService userService, IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor) {
        _userService = userService;
    }

    [HttpGet]
    [Route("api/limbo/migrations/users")]
    public object GetUsers() {

        if (!HasAccess(out string reason)) return Unauthorized(reason);

        return _userService
            .GetAll(0, int.MaxValue, out long _)
            .OrderBy(x => x.Id)
            .Select(x => new ApiUser(x));

    }

    [HttpGet]
    [Route("api/limbo/migrations/users/{id:int}")]
    public object GetUserById(int id) {

        if (!HasAccess(out string reason)) return Unauthorized(reason);

        IUser user = _userService.GetUserById(id);
        return user is null ? NotFound() : (object) new ApiUser(user);

    }

    [HttpGet]
    [Route("api/limbo/migrations/users/{key:guid}")]
    public object GetUserById(Guid key) {

        if (!HasAccess(out string reason)) return Unauthorized(reason);

        int userId = GuidUtils.ToInt32(key);

        IUser user = _userService.GetUserById(userId);
        return user is null ? NotFound() : (object) new ApiUser(user);

    }

    protected override bool HasAccess(out string rejectionReason) {

        if (!base.HasAccess(out rejectionReason)) return false;

        bool enabled = true; //WebConfigurationManager.AppSettings["LimboMigrationsApiUsersEnabled"].ToBoolean();
        if (enabled) return true;

        rejectionReason = "Users controller has not been enabled.";
        return false;

    }

}
