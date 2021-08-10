using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Primitives;
using NDQUAN.Core.Models.AuthModels;
using System;

namespace NDQUAN.Auth.API.Helpers
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = context.HttpContext.Items["UserInfo"];

            StringValues sessionId;
            context.HttpContext.Request.Headers.TryGetValue("SessionId", out sessionId);
            var session = SessionManager.Get(((user_member)user).user_id, sessionId.ToString());
            if(user == null || session == null)
            {
                context.Result = new JsonResult(new { messsage = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
            }


        }
    }
}
