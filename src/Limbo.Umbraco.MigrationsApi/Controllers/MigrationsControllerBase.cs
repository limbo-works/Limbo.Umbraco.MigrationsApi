using System.Text.RegularExpressions;
using Limbo.Umbraco.MigrationsApi.Models.Settings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Skybrud.Essentials.AspNetCore.Json.Newtonsoft;
using Skybrud.Essentials.AspNetCore.Json.Newtonsoft.Attributes;
using Skybrud.Essentials.Security;
using Skybrud.Essentials.Strings;
using Umbraco.Cms.Web.Common.Controllers;

namespace Limbo.Umbraco.MigrationsApi.Controllers;

[NewtonsoftJsonOnlyConfiguration]
public abstract class MigrationsControllerBase : UmbracoApiController {

    private readonly IOptions<MigrationsApiSettings> _options;

    public MigrationsApiSettings Settings => _options.Value;

    protected MigrationsControllerBase(IOptions<MigrationsApiSettings> options) {
        _options = options;
    }

    protected virtual bool HasAccess(out string rejectionReason) {

        if (string.IsNullOrWhiteSpace(Settings.ApiKey)) {
            rejectionReason = "No API key configured.";
            return false;
        }

        string auth = Request.Headers.Authorization;
        if (!RegexUtils.IsMatch(auth ?? string.Empty, "Basic (.+?)$", out Match m)) {
            rejectionReason = "No or invalid API key specified in request.";
            return false;
        }

        try {
            if (SecurityUtils.Base64Decode(m.Groups[1].Value) != $"api:{Settings.ApiKey}") {
                rejectionReason = "Invalid API key specified in request.";
                return false;
            }
        } catch {
            rejectionReason = "Meh";
            return false;
        }

        string? address = Request.HttpContext.Connection.RemoteIpAddress?.ToString();
        if (string.IsNullOrWhiteSpace(address)) {
            rejectionReason = "Meh 2";
            return false;
        }

        if (Settings.AllowList.Count == 0 || Settings.AllowList.Contains(address)) {
            rejectionReason = "Meh 3";
            return true;
        }

        rejectionReason = $"IP address '{address}' is not allowed.";
        return false;

    }

    protected IActionResult NotFound(string message) {
        return NewtonsoftJsonResult.NotFound(message);
    }

    protected IActionResult Unauthorized(string message) {
        return NewtonsoftJsonResult.Unauthorized(message);
    }

}