using EducacaoOnline.Api.Models;
using EducacaoOnline.Api.Tests.Configuration;
using EducacaoOnline.Core.Tests;
using System.Net.Http.Json;

namespace EducacaoOnline.Api.Tests.V1.Autenticacao
{
    [TestCaseOrderer("Features.Tests.PriorityOrderer", "Features.Tests")]
    [Collection(nameof(IntegrationApiTestsFixtureCollection))]
    public class AutenticacaoControllerTests
    {
        RegistroUsuarioModel _registroUsuarioValido;
        private readonly IntegrationTestsFixture<Program> _testsFixture;
        private const string URIAutenticacao = "/api/v1/autenticacao";
        private const string URILogin = $"{URIAutenticacao}/login";
        private const string URIRegistro = $"{URIAutenticacao}/registrar";

        public AutenticacaoControllerTests(IntegrationTestsFixture<Program> testsFixture)
        {
            // Arrange
            _testsFixture = testsFixture;
            _testsFixture.GerarSenhaUsuario();

            _registroUsuarioValido = new RegistroUsuarioModel(
                email: _testsFixture.UsuarioEmail,
                password: _testsFixture.UsuarioSenha,
                confirmPassword: _testsFixture.UsuarioSenha);
        }

        [Fact(DisplayName = "Registrar usuário, senha forte, retorna sucesso"), TestPriority(1)]
        [Trait("Categoria", "Integração API - Autenticação")]
        public async Task Registrar_UsuarioComSenhaForte_DeveRetornarSucesso()
        {
            // Act
            var postResponse = await _testsFixture.Client.PostAsJsonAsync(URIRegistro, _registroUsuarioValido);
            await postResponse.Content.ReadAsStringAsync();

            // Assert
            postResponse.EnsureSuccessStatusCode();
        }

        [Fact(DisplayName = "Registrar usuário, já existente, retorna erro"), TestPriority(2)]
        [Trait("Categoria", "Integração API - Autenticação")]
        public async Task Registrar_UsuarioJaExistente_DeveRetornarErro()
        {
            // Arrange
            await _testsFixture.Client.PostAsJsonAsync(URIRegistro, _registroUsuarioValido);

            // Act
            var postResponse = await _testsFixture.Client.PostAsJsonAsync(URIRegistro, _registroUsuarioValido);
            await postResponse.Content.ReadAsStringAsync();

            // Assert
            await postResponse.Content.ReadAsStringAsync();
            Assert.Equal(System.Net.HttpStatusCode.BadRequest, postResponse.StatusCode);
            var responseString = await postResponse.Content.ReadAsStringAsync();
            Assert.Contains("is already taken", responseString);
        }

        [Fact(DisplayName = "Registrar usuário, senha fraca, retorna erro"), TestPriority(5)]
        [Trait("Categoria", "Integração API - Autenticação")]
        public async Task Registrar_UsuarioComSenhaFraca_DeveRetornarErro()
        {
            // Arrange
            _testsFixture.GerarSenhaUsuario();
            var registroUsuario = new RegistroUsuarioModel(
                email: _testsFixture.UsuarioEmail,
                password: "123", 
                confirmPassword: "123");

            // Act
            var postResponse = await _testsFixture.Client.PostAsJsonAsync(URIRegistro, registroUsuario);

            // Assert
            await postResponse.Content.ReadAsStringAsync();
            Assert.Equal(System.Net.HttpStatusCode.BadRequest, postResponse.StatusCode);
            var responseString = await postResponse.Content.ReadAsStringAsync();
            Assert.Contains("O campo Password deve ter entre 6 e 100 caracteres.", responseString);
        }

        [Fact(DisplayName = "Fazer login, dados corretos, retorna sucesso"), TestPriority(3)]
        [Trait("Categoria", "Integração API - Autenticação")]
        public async Task FazerLogin_DadosCorretos_DeveRetornarComSucesso()
        {
            // Arrange
            var loginUsuario = new LoginUsuarioModel(
                email: "admin@teste.com",
                password: "Teste@123");

            // Act
            var postResponse = await _testsFixture.Client.PostAsJsonAsync(URILogin, loginUsuario);
            await postResponse.Content.ReadAsStringAsync();

            // Assert
            postResponse.EnsureSuccessStatusCode();
        }

        [Fact(DisplayName = "Fazer login, dados incorretos, retorna erro"), TestPriority(4)]
        [Trait("Categoria", "Integração API - Autenticação")]
        public async Task FazerLogin_DadosIncorretos_DeveRetornarComErro()
        {
            // Arrange
            var loginUsuario = new LoginUsuarioModel(
                email: "admin@teste.com",
                password: "errada");

            // Act
            var postResponse = await _testsFixture.Client.PostAsJsonAsync(URILogin, loginUsuario);
            await postResponse.Content.ReadAsStringAsync();

            // Assert
            await postResponse.Content.ReadAsStringAsync();
            Assert.Equal(System.Net.HttpStatusCode.BadRequest, postResponse.StatusCode);
            var responseString = await postResponse.Content.ReadAsStringAsync();
            Assert.Contains("Usuário ou senha inválidos", responseString);
        }
    }
}