using EducacaoOnline.Api.Models;
using EducacaoOnline.Api.Tests.Configuration;
using Newtonsoft.Json;
using System.Text;

namespace EducacaoOnline.Api.Tests
{
    [TestCaseOrderer("Features.Tests.PriorityOrderer", "Features.Tests")]
    [Collection(nameof(IntegrationWebTestsFixtureCollection))]
    public class ApiContaTests
    {
        private readonly IntegrationTestsFixture<Program> _testsFixture;
        private const string URIConta = "/api/conta";
        private const string URILogin = $"{URIConta}/login";
        private const string URIRegistro = $"{URIConta}/registrar";

        public ApiContaTests(IntegrationTestsFixture<Program> testsFixture)
        {
            _testsFixture = testsFixture;
        }

        [Fact(DisplayName = "Registrar usuário com sucesso"), TestPriority(1)]
        [Trait("Categoria", "Integração API - Usuário")]
        public async Task Usuario_RealizarCadastro_DeveExecutarComSucesso()
        {
            // Arrange
            var registroUsuario = new RegistroUsuarioViewModel(
                email: "sucesso@teste.com", 
                password: "Teste@123", 
                confirmPassword: "Teste@123");
            
            var stringRegistroUsuario = JsonConvert.SerializeObject(registroUsuario);
            var httpContent = new StringContent(stringRegistroUsuario, Encoding.UTF8, "application/json");

            // Act
            var postResponse = await _testsFixture.Client.PostAsync(URIRegistro, httpContent);
            await postResponse.Content.ReadAsStringAsync();

            // Assert
            postResponse.EnsureSuccessStatusCode();
        }

        [Fact(DisplayName = "Registrar usuário com senha fraca"), TestPriority(3)]
        [Trait("Categoria", "Integração API - Usuário")]
        public async Task Usuario_RealizarCadastroComSenhaFraca_DeveRetornarMensagemDeErro()
        {
            // Arrange
            var registroUsuario = new RegistroUsuarioViewModel(
                email: "insucesso@teste.com",
                password: "123", 
                confirmPassword: "123");

            var stringRegistroUsuario = JsonConvert.SerializeObject(registroUsuario);
            var httpContent = new StringContent(stringRegistroUsuario, Encoding.UTF8, "application/json");

            // Act
            var postResponse = await _testsFixture.Client.PostAsync(URIRegistro, httpContent);

            // Assert
            await postResponse.Content.ReadAsStringAsync();
            Assert.Equal(System.Net.HttpStatusCode.BadRequest, postResponse.StatusCode);
            var responseString = await postResponse.Content.ReadAsStringAsync();
            Assert.Contains("O campo Password deve ter entre 6 e 100 caracteres.", responseString);
        }

        [Fact(DisplayName = "Fazer login com sucesso"), TestPriority(2)]
        [Trait("Categoria", "Integração API - Usuário")]
        public async Task Usuario_RealizarLogin_DeveExecutarComSucesso()
        {
            // Arrange
            var loginUsuario = new LoginUsuarioViewModel(
                email: "sucesso@teste.com",
                password: "Teste@123");

            var stringLoginUsuario = JsonConvert.SerializeObject(loginUsuario);
            var httpContent = new StringContent(stringLoginUsuario, Encoding.UTF8, "application/json");

            // Act
            var postResponse = await _testsFixture.Client.PostAsync(URILogin, httpContent);
            await postResponse.Content.ReadAsStringAsync();

            // Assert
            postResponse.EnsureSuccessStatusCode();
        }
    }
}