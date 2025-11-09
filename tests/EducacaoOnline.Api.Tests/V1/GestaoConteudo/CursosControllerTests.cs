using EducacaoOnline.Api.Models;
using EducacaoOnline.Api.Tests.Configuration;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Json;

namespace EducacaoOnline.Api.Tests.V1.GestaoConteudo
{
    [Collection(nameof(IntegrationApiTestsFixtureCollection))]
    public class CursosControllerTests
    {
        private readonly IntegrationTestsFixture<Program> _testsFixture;
        private const string URIGestaoConteudo = "/api/v1/gestao-conteudo";
        private const string URICursos = $"{URIGestaoConteudo}/cursos";

        public CursosControllerTests(IntegrationTestsFixture<Program> testsFixture)
        {
            _testsFixture = testsFixture;
        }

        [Fact(DisplayName = "Incluir curso, válido, grava curso")]
        [Trait("Categoria", "Integração API - Gestão Conteúdo - Cursos")]
        public async Task IncluirCurso_Valido_DeveGravarCurso()
        {
            // Arrange
            await _testsFixture.FazerLoginAdministrador();

            var cursoModel = new CursoModel
            {
                Nome = "Curso de Teste",
                Descricao = "Descrição do Curso de Teste",
            };

            // Act
            var postResponse = await _testsFixture.Client.PostAsJsonAsync(URICursos, cursoModel);
            await postResponse.Content.ReadAsStringAsync();

            // Assert
            Assert.Equal(StatusCodes.Status201Created, (int)postResponse.StatusCode);
        }
    }
}