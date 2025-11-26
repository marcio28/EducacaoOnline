using System.Net;
using System.Security.Claims;
using EducacaoOnline.Api.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;

namespace EducacaoOnline.Api.Tests.Extensions
{
    public class RequisitoDeclaracaoFilterTests
    {
        [Fact(DisplayName = "OnAuthorization, identidade nula, lança exceção")]
        [Trait("Categoria", "API Extensões - RequisitoDeclaracaoFilter")]
        public void OnAuthorization_IdentidadeNula_DeveLancarExcecao()
        {
            // Arrange
            var httpContext = new DefaultHttpContext
            {
                User = new ClaimsPrincipal() // sem identidades -> Identity é nula
            };

            var actionContext = new ActionContext(httpContext, new RouteData(), new ActionDescriptor());

            var authContext = new AuthorizationFilterContext(actionContext, []);

            var filtro = new RequisitoDeclaracaoFilter(new Claim("Cursos", "Cadastrar"));

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => filtro.OnAuthorization(authContext));
        }

        [Fact(DisplayName = "OnAuthorization, usuário não autenticado, retorna não autorizado")]
        [Trait("Categoria", "API Extensões - RequisitoDeclaracaoFilter")]
        public void OnAuthorization_UsuarioNaoAutenticado_DeveRetornarNaoAutorizado()
        {
            // Arrange
            var httpContext = new DefaultHttpContext();
            var identity = new ClaimsIdentity(); // não autenticado
            httpContext.User = new ClaimsPrincipal(identity);

            var actionContext = new ActionContext(httpContext, new RouteData(), new ActionDescriptor());
            var authContext = new AuthorizationFilterContext(actionContext, []);

            var filtro = new RequisitoDeclaracaoFilter(new Claim("Cursos", "Cadastrar"));

            // Act
            filtro.OnAuthorization(authContext);

            // Assert
            Assert.IsType<UnauthorizedResult>(authContext.Result);
        }

        [Fact(DisplayName = "OnAuthorization, usuário sem declaração, retorna acesso negado")]
        [Trait("Categoria", "API Extensões - RequisitoDeclaracaoFilter")]
        public void OnAuthorization_UsuarioSemDeclaracao_DeveRetornarAcessoNegado()
        {
            var httpContext = new DefaultHttpContext();
            var identidade = new ClaimsIdentity(
                [ new Claim("Cursos", "OUTRO_VALOR") ], "TestAuth"); // autenticado mas não contém declaração autorizante

            httpContext.User = new ClaimsPrincipal(identidade);

            var actionContext = new ActionContext(httpContext, new RouteData(), new ActionDescriptor());
            var authContext = new AuthorizationFilterContext(actionContext, []);

            var filtro = new RequisitoDeclaracaoFilter(new Claim("Cursos", "Cadastrar"));

            filtro.OnAuthorization(authContext);

            var resultado = Assert.IsType<StatusCodeResult>(authContext.Result);
            Assert.Equal((int)HttpStatusCode.Forbidden, resultado.StatusCode);
        }

        [Fact(DisplayName = "OnAuthorization, usuário autenticado e com declaração, autoriza")]
        [Trait("Categoria", "API Extensões - RequisitoDeclaracaoFilter")]
        public void OnAuthorization_UsuarioAutenticadoEComDeclaracao_DeveAutorizar()
        {
            var httpContext = new DefaultHttpContext();
            var identidade = new ClaimsIdentity(
                [ new Claim("Cursos", "MatricularSe,Finalizar,Cadastrar") ], "TestAuth");

            httpContext.User = new ClaimsPrincipal(identidade);

            var actionContext = new ActionContext(httpContext, new RouteData(), new ActionDescriptor());
            var authContext = new AuthorizationFilterContext(actionContext, []);

            var filtro = new RequisitoDeclaracaoFilter(new Claim("Cursos", "Cadastrar"));

            filtro.OnAuthorization(authContext);

            Assert.Null(authContext.Result); // sem resultado -> autorizado
        }
    }
}