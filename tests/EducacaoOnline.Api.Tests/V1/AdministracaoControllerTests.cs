using EducacaoOnline.Api.Models;
using EducacaoOnline.Api.Tests.Configuration;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Text;

namespace EducacaoOnline.Api.Tests.V1
{
    [Collection(nameof(IntegrationWebTestsFixtureCollection))]
    public class AdministracaoControllerTests
    {
        private readonly IntegrationTestsFixture<Program> _testsFixture;
        private const string URIAdministracao = "/api/V1/admin";
        private const string URICursos = $"{URIAdministracao}/cursos";

        public AdministracaoControllerTests(IntegrationTestsFixture<Program> testsFixture)
        {
            _testsFixture = testsFixture;
        }

        [Fact(DisplayName = "Incluir Curso Válido Deve Retornar 201 Created Quando serviço grava curso")]
        [Trait("Categoria", "Integração API - Administração - Cursos")]
        public async Task IncluirCurso_DeveRetornarCreated_QuandoServicoGravaCurso()
        {
            // Arrange
            var cursoModel = new CursoModel
            {
                Nome = "Curso de Teste",
                Descricao = "Descrição do Curso de Teste",
            };

            var stringCurso = JsonConvert.SerializeObject(cursoModel);
            var httpContent = new StringContent(stringCurso, Encoding.UTF8, "application/json");

            // Act
            var postResponse = await _testsFixture.Client.PostAsync(URICursos, httpContent);
            await postResponse.Content.ReadAsStringAsync();

            // Assert
            Assert.Equal(StatusCodes.Status201Created, ((int)postResponse.StatusCode));
        }
    }
}