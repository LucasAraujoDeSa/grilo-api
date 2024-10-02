using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Grilo.Api.Attributes
{
    public class CustomAuthorizeAttribute() : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var token = context.
                HttpContext.
                Request.
                Headers.
                Authorization.
                FirstOrDefault()?.
                Split(" ").
                Last();

            if (token is null)
            {
                context.Result = new UnauthorizedResult();
                return;
            }
        }
    }
}