using System.Security.Claims;
using EducacaoOnline.Api.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace EducacaoOnline.Api.Tests.Extensions
{
    public class DeclaracaoAutorizanteAttributeTests
    {
        [Fact(DisplayName = "DeclaracaoAutorizanteAttribute, com argumentos, retorna declaração")]
        [Trait("Categoria", "API Extensões")]
        public void DeclaracaoAutorizanteAttribute_ComArgumentos_DeveRetornarDeclaracao()
        {
            // Arrange
            var nomeDeclaracao = "Cursos";
            var valorDeclaracao = "Cadastrar";

            // Act
            var atributo = new DeclaracaoAutorizanteAttribute(nomeDeclaracao, valorDeclaracao);

            // Assert
            Assert.IsType<DeclaracaoAutorizanteAttribute>(atributo);
            var atributoTypeFilter = Assert.IsAssignableFrom<TypeFilterAttribute>(atributo);

            Assert.NotNull(atributoTypeFilter.Arguments);
            Assert.Single(atributoTypeFilter.Arguments);

            var declaracao = Assert.IsType<Claim>(atributoTypeFilter.Arguments[0]);
            Assert.Equal(nomeDeclaracao, declaracao.Type);
            Assert.Equal(valorDeclaracao, declaracao.Value);
        }
    }
}