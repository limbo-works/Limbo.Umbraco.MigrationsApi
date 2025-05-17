using System.Net;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Skybrud.Essentials.AspNetCore.Json.Newtonsoft.Attributes;
using Skybrud.Essentials.Security;
using Skybrud.Essentials.Strings;
using Skybrud.WebApi.Json.Meta;
using Umbraco.Cms.Web.Common.Controllers;

namespace Limbo.Umbraco.MigrationsApi.Controllers;

[NewtonsoftJsonOnlyConfigurationAttribute]
public abstract class MigrationsControllerBase : UmbracoApiController {

    private readonly string _apiKey;
    private readonly HashSet<string> _allowList;

    protected MigrationsControllerBase() {
        _apiKey = "temp"; //WebConfigurationManager.AppSettings["LimboMigrationsApiKey"];
        _allowList = new HashSet<string>(); //WebConfigurationManager.AppSettings["LimboMigrationsApiAllowList"].ToStringArray().ToHashSet();
    }

    protected virtual bool HasAccess(out string rejectionReason) {

        if (string.IsNullOrWhiteSpace(_apiKey)) {
            rejectionReason = "No API key configured.";
            return false;
        }

        string auth = Request.Headers.Authorization;
        if (!RegexUtils.IsMatch(auth ?? string.Empty, "Basic (.+?)$", out Match m)) {
            rejectionReason = "No or invalid API key specified in request.";
            return false;
        }

        try {
            if (SecurityUtils.Base64Decode(m.Groups[1].Value) != $"api:{_apiKey}") {
                rejectionReason = "Invalid API key specified in request.";
                return false;
            }
        } catch {
            rejectionReason = "Meh";
            return false;
        }

        string? addr = Request.HttpContext.Connection.RemoteIpAddress?.ToString();
        if (string.IsNullOrWhiteSpace(addr)) {
            rejectionReason = "Meh 2";
            return false;
        }

        if (_allowList.Count == 0 || _allowList.Contains(addr)) {
            rejectionReason = "Meh 3";
            return true;
        }

        rejectionReason = $"IP address '{addr}' is not allowed.";
        return false;

    }

    protected IActionResult Unauthorized(string message) {
        var body = JsonMetaResponse.GetError(HttpStatusCode.Unauthorized, message);
        return StatusCode((int) HttpStatusCode.Unauthorized, body);
    }

}
