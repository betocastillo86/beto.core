namespace Beto.Core.Web.Api
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Security.Claims;
    using Microsoft.AspNetCore.Http;

    public static class LogDetailedFactory
    {
        public static LogDetailedModel GetModel(
            Exception exception,
            HttpContext context)
        {
            var detail = new LogDetailedModel
            {
                AdditionalInfo = new Dictionary<string, object>()
            };

            LogDetailedFactory.SetUserData(detail, context);
            LogDetailedFactory.SetRequestData(detail, context);

            return detail;
        }

        private static void SetRequestData(LogDetailedModel detail, HttpContext context)
        {
            var request = context.Request;
            if (request != null)
            {
                detail.Location = request.Path;

                foreach (var header in request.Headers)
                {
                    if (!header.Key.Equals("Authorization", StringComparison.OrdinalIgnoreCase))
                    {
                        detail.AdditionalInfo.Add($"Header-{header.Key}", header.Value);
                    }
                }

                if (request.Body != null)
                {
                    try
                    {
                        detail.AdditionalInfo.Add("Body", new StreamReader(request.Body).ReadToEnd());
                    }
                    catch { }
                }

                var qdict = Microsoft.AspNetCore.WebUtilities
                    .QueryHelpers.ParseQuery(request.QueryString.ToString());

                foreach (var key in qdict.Keys)
                {
                    detail.AdditionalInfo.Add($"QueryString-{key}", qdict[key]);
                }
            }
        }

        private static void SetUserData(LogDetailedModel detail, HttpContext context)
        {
            var userId = string.Empty;
            var userName = string.Empty;
            var user = context.User;

            if (user != null)
            {
                var i = 1;
                foreach (var claim in user.Claims)
                {
                    if (claim.Type == ClaimTypes.NameIdentifier)
                    {
                        userId = claim.Value;
                    }
                    else if (claim.Type == "name")
                    {
                        userName = claim.Value;
                    }
                    else
                    {
                        detail.AdditionalInfo.Add($"UserClaim-{i++}-{claim.Type}", claim.Value);
                    }
                }
            }

            detail.UserId = userId;
            detail.UserName = userName;
        }
    }
}