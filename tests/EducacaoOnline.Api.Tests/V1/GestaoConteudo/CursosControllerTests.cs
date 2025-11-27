using EducacaoOnline.Api.Controllers;
using EducacaoOnline.Api.Tests.Configuration;
using EducacaoOnline.Api.Tests.Extensions;
using EducacaoOnline.GestaoConteudo.Application.Models;
using Microsoft.AspNetCore.Http;
using System.Net;
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
        [Trait("Categoria", "Integração API - Gestão de Conteúdo - Cursos")]
        public async Task IncluirCurso_Valido_DeveGravarCurso()
        {
            // Arrange
            await _testsFixture.FazerLoginAdministrador();
            _testsFixture.Client.AtribuirToken(_testsFixture.UsuarioToken);

            var id = Guid.NewGuid();
            var cursoModel = new CursoModel
            {
                Id = id,
                Nome = $"Curso de Teste {id}",
                Descricao = "Descrição do Curso de Teste",
            };

            // Act
            var postResponse = await _testsFixture.Client.PostAsJsonAsync(URICursos, cursoModel);
            await postResponse.Content.ReadAsStringAsync();

            // Assert
            Assert.Equal(StatusCodes.Status201Created, (int)postResponse.StatusCode);
        }

        [Fact(DisplayName = "Incluir curso, inválido, returna insucesso")]
        [Trait("Categoria", "Integração API - Gestão de Conteúdo - Cursos")]
        public async Task IncluirCurso_Invalido_DeveRetornarInsucesso()
        {
            // Arrange
            await _testsFixture.FazerLoginAdministrador();
            _testsFixture.Client.AtribuirToken(_testsFixture.UsuarioToken);

            var cursoModel = new CursoModel
            {
                Nome = "",
                Descricao = "",
            };

            // Act
            var postResponse = await _testsFixture.Client.PostAsJsonAsync(URICursos, cursoModel);
            await postResponse.Content.ReadAsStringAsync();

            // Assert
            Assert.Equal(StatusCodes.Status400BadRequest, (int)postResponse.StatusCode);
        }

        [Fact(DisplayName = "Listar cursos, curso criado, retorna Ok e lista contendo curso")]
        [Trait("Categoria", "Integração API - Gestão de Conteúdo - Cursos")]
        public async Task ListarCursos_CursoCriado_DeveRetornarOkEListaContendoCurso()
        {
            // Arrange
            await _testsFixture.FazerLoginAdministrador();
            _testsFixture.Client.AtribuirToken(_testsFixture.UsuarioToken);

            var id = Guid.NewGuid();
            var cursoModel = new CursoModel
            {
                Id = id,
                Nome = $"Curso Listar Teste {id}",
                Descricao = "Descrição Listar"
            };

            var postResponse = await _testsFixture.Client.PostAsJsonAsync(URICursos, cursoModel);
            Assert.Equal(HttpStatusCode.Created, postResponse.StatusCode);

            // Act
            var getResponse = await _testsFixture.Client.GetAsync(URICursos);
            var wrapper = await getResponse.Content.ReadFromJsonAsync<ApiResponse<IEnumerable<CursoModel>>>();
            var cursos = wrapper?.data;

            // Assert
            Assert.Equal(HttpStatusCode.OK, getResponse.StatusCode);
            Assert.NotNull(cursos);
            Assert.Contains(cursos, c => c.Id == id && c.Nome == cursoModel.Nome);
        }

        [Fact(DisplayName = "Alterar curso, válido, retorna NoContent")]
        [Trait("Categoria", "Integração API - Gestão de Conteúdo - Cursos")]
        public async Task AlterarCurso_CursoValido_DeveRetornarNoContent()
        {
            // Arrange
            await _testsFixture.FazerLoginAdministrador();
            _testsFixture.Client.AtribuirToken(_testsFixture.UsuarioToken);

            var id = Guid.NewGuid();
            var cursoModel = new CursoModel
            {
                Id = id,
                Nome = $"Curso Alterar Teste {id}",
                Descricao = "Descrição Alterar Original"
            };

            var postResponse = await _testsFixture.Client.PostAsJsonAsync(URICursos, cursoModel);
            Assert.Equal(HttpStatusCode.Created, postResponse.StatusCode);

            // Act
            cursoModel.Nome = $"Curso Alterado Nome {id}";
            cursoModel.Descricao = "Descrição Alterada";
            var putResponse = await _testsFixture.Client.PutAsJsonAsync($"{URICursos}/{id}", cursoModel);

            // Assert
            Assert.Equal(HttpStatusCode.NoContent, putResponse.StatusCode);
        }

        [Fact(DisplayName = "Obter curso por id, id existente, retorna OK com curso")]
        [Trait("Categoria", "Integração API - Gestão de Conteúdo - Cursos")]
        public async Task ObterCursoPorId_IdExistente_DeveRetornarOkComCurso()
        {
            // Arrange
            await _testsFixture.FazerLoginAdministrador();
            _testsFixture.Client.AtribuirToken(_testsFixture.UsuarioToken);

            var id = Guid.NewGuid();
            var cursoModel = new CursoModel
            {
                Id = id,
                Nome = $"Curso Obter Teste {id}",
                Descricao = "Descrição Obter"
            };

            var postResponse = await _testsFixture.Client.PostAsJsonAsync(URICursos, cursoModel);
            Assert.Equal(HttpStatusCode.Created, postResponse.StatusCode);

            // Act
            var getResponse = await _testsFixture.Client.GetAsync($"{URICursos}/{id}");
            var wrapper = await getResponse.Content.ReadFromJsonAsync<ApiResponse<CursoModel>>();
            var cursoObtido = wrapper?.data;

            // Assert
            Assert.Equal(HttpStatusCode.OK, getResponse.StatusCode);
            Assert.NotNull(cursoObtido);
            Assert.Equal(cursoModel.Id, cursoObtido.Id);
            Assert.Equal(cursoModel.Nome, cursoObtido.Nome);
        }

        [Fact(DisplayName = "Excluir curso, id existente, retorna NoContent")]
        [Trait("Categoria", "Integração API - Gestão de Conteúdo - Cursos")]
        public async Task ExcluirCurso_IdExistente_DeveRetornarNoContent()
        {
            // Arrange
            await _testsFixture.FazerLoginAdministrador();
            _testsFixture.Client.AtribuirToken(_testsFixture.UsuarioToken);

            var id = Guid.NewGuid();
            var cursoModel = new CursoModel
            {
                Id = id,
                Nome = $"Curso Excluir Teste {id}",
                Descricao = "Descrição Excluir"
            };

            var postResponse = await _testsFixture.Client.PostAsJsonAsync(URICursos, cursoModel);
            Assert.Equal(HttpStatusCode.Created, postResponse.StatusCode);

            // Act
            var deleteResponse = await _testsFixture.Client.DeleteAsync($"{URICursos}/{id}");

            // Assert
            Assert.Equal(HttpStatusCode.NoContent, deleteResponse.StatusCode);
        }
    }
}