using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using VigneCommerce.Api.Response.Base;

namespace VigneCommerce.Api.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class CustomAuthorize : Attribute, IAuthorizationFilter
    {
        public string[] Roles { get; set; }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (!context.HttpContext.User.Identity.IsAuthenticated)
            {
                context.Result = new JsonResult(new ResponseBase(false, "Não autenticado."))
                {
                    StatusCode = 401
                };
                return;
            }

            if (!IsInRole(context))
            {
                context.Result = new JsonResult(new ResponseBase(false, "Não autorizado."))
                {
                    StatusCode = 403
                };
                return;
            }
        }

        private bool IsInRole(AuthorizationFilterContext context)
        {
            foreach (var role in Roles)
                if (context.HttpContext.User.IsInRole(role))
                    return true;

            return false;
        }
    }
}
