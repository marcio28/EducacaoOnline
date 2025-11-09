using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace EducacaoOnline.Api.Extensions
{
    public class RequisitoClaimFilter(Claim claim) : IAuthorizationFilter
    {
        private readonly Claim _claim = claim;

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (context.HttpContext.User.Identity is null) throw new InvalidOperationException();

            if (context.HttpContext.User.Identity.IsAuthenticated is false)
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            if (ClaimsAuthorization.ValidarClaimsUsuario(context.HttpContext, _claim.Type, _claim.Value) is false)
                context.Result = new StatusCodeResult(403);
        }
    }
}