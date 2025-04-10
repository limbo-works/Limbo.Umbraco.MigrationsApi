using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Configuration;
using System.Web.Http;
using Skybrud.Essentials.Security;
using Skybrud.Essentials.Strings;
using Skybrud.Essentials.Strings.Extensions;
using Skybrud.WebApi.Json;
using Skybrud.WebApi.Json.Meta;
using Umbraco.Web.WebApi;

namespace Limbo.Umbraco.MigrationsApi.Controllers {

    [JsonOnlyConfiguration]
    public abstract class MigrationsControllerBase : UmbracoApiController {

        private readonly string _apiKey;
        private readonly HashSet<string> _allowList;

        protected MigrationsControllerBase() {
            _apiKey = WebConfigurationManager.AppSettings["LimboMigrationsApiKey"];
            _allowList = WebConfigurationManager
                .AppSettings["LimboMigrationsApiAllowList"]
                .ToStringArray()
                .ToHashSet();
        }

        protected virtual bool HasAccess(out string rejectionReason) {

            if (string.IsNullOrWhiteSpace(_apiKey)) {
                rejectionReason = "No API key configured.";
                return false;
            }

            string auth = HttpContext.Current.Request.Headers["Authorization"];
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

            string addr = HttpContext.Current.Request.ServerVariables.Get("REMOTE_ADDR");
            if (string.IsNullOrWhiteSpace(addr)) {
                rejectionReason = "Meh";
                return false;
            }

            if (_allowList.Count == 0 || _allowList.Contains(addr)) {
                rejectionReason = null;
                return true;
            }

            rejectionReason = $"IP address '{addr}' is not allowed.";
            return false;

        }

        protected IHttpActionResult Unauthorized(string message) {
            var body = JsonMetaResponse.GetError(HttpStatusCode.Unauthorized, message);
            return Content(HttpStatusCode.Unauthorized, body);
        }

    }

}