using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace EducacaoOnline.Api.Extensions
{
    public class RequisitoDeclaracaoFilter(Claim declaracao) : IAuthorizationFilter
    {
        private readonly Claim _declaracao = declaracao;

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (context.HttpContext.User.Identity is null) throw new InvalidOperationException();

            if (context.HttpContext.User.Identity.IsAuthenticated is false)
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            if (AutorizacaoDeclaracoes.ValidarDeclaracoesUsuario(context.HttpContext, _declaracao.Type, _declaracao.Value) is false)
                context.Result = new StatusCodeResult(403);
        }
    }
}