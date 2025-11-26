using EducacaoOnline.Api.Extensions;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace EducacaoOnline.Api.Tests.Extensions
{
    public class AutorizacaoDeclaracoesTests
    {
        [Fact(DisplayName = "ValidarDeclaracoesUsuario, usuário com declaração, é autorizado")]
        [Trait("Categoria", "API Extensões - AutorizacaoDeclaracoes")]
        public void ValidarDeclaracoesUsuario_UsuarioComDeclaracao_DeveSerAutorizado()
        {
            // Arrange
            var contexto = new DefaultHttpContext();
            var declaracoes = new[] { new Claim("Cursos", "Cadastrar") };
            var identidade = new ClaimsIdentity(declaracoes, "TestAuth");
            contexto.User = new ClaimsPrincipal(identidade);

            // Act
            var temAutorizacao = AutorizacaoDeclaracoes.ValidarDeclaracoesUsuario(contexto, "Cursos", "Cadastrar");

            // Assert
            Assert.True(temAutorizacao);
        }

        [Fact(DisplayName = "ValidarDeclaracoesUsuario, usuário com declarações, é autorizado")]
        [Trait("Categoria", "API Extensões - AutorizacaoDeclaracoes")]
        public void ValidarDeclaracoesUsuario_UsuarioComDeclaracoes_DeveSerAutorizado()
        {
            // Arrange
            var contexto = new DefaultHttpContext();
            var declaracoes = new[] { new Claim("Cursos", "MatricularSe,Finalizar") };
            var identidade = new ClaimsIdentity(declaracoes, "TestAuth");
            contexto.User = new ClaimsPrincipal(identidade);

            // Act
            var temAutorizacao = AutorizacaoDeclaracoes.ValidarDeclaracoesUsuario(contexto, "Cursos", "Finalizar");

            // Assert
            Assert.True(temAutorizacao);
        }

        [Fact(DisplayName = "ValidarDeclaracoesUsuario, usuário não autenticado, não é autorizado")]
        [Trait("Categoria", "API Extensões - AutorizacaoDeclaracoes")]
        public void ValidarDeclaracoesUsuario_UsuarioNaoAutenticado_DeveNaoSerAutorizado()
        {
            // Arrange
            var contexto = new DefaultHttpContext();
            var declaracoes = new[] { new Claim("Cursos", "Cadastrar") };
            var identidade = new ClaimsIdentity(declaracoes); // no authentication type => not authenticated
            contexto.User = new ClaimsPrincipal(identidade);

            // Act
            var temAutorizacao = AutorizacaoDeclaracoes.ValidarDeclaracoesUsuario(contexto, "Cursos", "Cadastrar");

            // Assert
            Assert.False(temAutorizacao);
        }

        [Fact(DisplayName = "ValidarDeclaracoesUsuario, usuário sem declaração, não é autorizado")]
        [Trait("Categoria", "API Extensões - AutorizacaoDeclaracoes")]
        public void ValidarDeclaracoesUsuario_UsuarioSemDeclaracao_DeveNaoSerAutorizado()
        {
            // Arrange
            var contexto = new DefaultHttpContext();
            var declaracoes = new[] { new Claim("Cursos", "Cadastrar") };
            var identidade = new ClaimsIdentity(declaracoes, "TestAuth");
            contexto.User = new ClaimsPrincipal(identidade);

            // Act
            var temAutorizacao = AutorizacaoDeclaracoes.ValidarDeclaracoesUsuario(contexto, "Cursos", "Gerir");

            // Assert
            Assert.False(temAutorizacao);
        }
    }
}