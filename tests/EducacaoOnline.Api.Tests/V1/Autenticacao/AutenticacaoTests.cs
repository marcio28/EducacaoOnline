using EducacaoOnline.Api.Models;
using EducacaoOnline.Api.Tests.Configuration;
using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Text;

namespace EducacaoOnline.Api.Tests.V1.Autenticacao
{
    [TestCaseOrderer("Features.Tests.PriorityOrderer", "Features.Tests")]
    [Collection(nameof(IntegrationApiTestsFixtureCollection))]
    public class AutenticacaoTests
    {
        private readonly IntegrationTestsFixture<Program> _testsFixture;
        private const string URIAutenticacao = "/api/V1/autenticacao";
        private const string URILogin = $"{URIAutenticacao}/login";
        private const string URIRegistro = $"{URIAutenticacao}/registrar";

        public AutenticacaoTests(IntegrationTestsFixture<Program> testsFixture)
        {
            _testsFixture = testsFixture;
        }

        [Fact(DisplayName = "Registrar usuário, senha forte, retorna sucesso"), TestPriority(1)]
        [Trait("Categoria", "Integração API - Autenticação")]
        public async Task Registrar_UsuarioComSenhaForte_DeveRetornarSucesso()
        {
            // Arrange
            var registroUsuario = new RegistroUsuarioViewModel(
                email: "sucesso@teste.com", 
                password: "Teste@123", 
                confirmPassword: "Teste@123");
            
            // Act
            var postResponse = await _testsFixture.Client.PostAsJsonAsync(URIRegistro, registroUsuario);
            await postResponse.Content.ReadAsStringAsync();

            // Assert
            postResponse.EnsureSuccessStatusCode();
        }

        [Fact(DisplayName = "Registrar usuário, senha fraca, retorna erro"), TestPriority(3)]
        [Trait("Categoria", "Integração API - Autenticação")]
        public async Task Registrar_UsuarioComSenhaFraca_DeveRetornarErro()
        {
            // Arrange
            var registroUsuario = new RegistroUsuarioViewModel(
                email: "insucesso@teste.com",
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

        [Fact(DisplayName = "Fazer login, dados corretos, retorna sucesso"), TestPriority(2)]
        [Trait("Categoria", "Integração API - Autenticação")]
        public async Task FazerLogin_DadosCorretos_DeveRetornarComSucesso()
        {
            // Arrange
            var loginUsuario = new LoginUsuarioViewModel(
                email: "sucesso@teste.com",
                password: "Teste@123");

            // Act
            var postResponse = await _testsFixture.Client.PostAsJsonAsync(URILogin, loginUsuario);
            await postResponse.Content.ReadAsStringAsync();

            // Assert
            postResponse.EnsureSuccessStatusCode();
        }
    }
}