using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Grilo.Api.Attributes
{
    public class CustomAuthorizeAttribute : Attribute, IAuthorizationFilter
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
            else
            {
                var handler = new JwtSecurityTokenHandler();
                var accountId = handler.ReadJwtToken(token).Claims.FirstOrDefault(item => item.Type == "id");
                if (accountId is null)
                {
                    context.Result = new UnauthorizedResult();
                    return;
                }
                context.HttpContext.Items["accountId"] = accountId.Value;
            }
        }
    }
}