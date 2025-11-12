using Bogus;
using EducacaoOnline.Api.Models;
using System.Net.Http.Json;

namespace EducacaoOnline.Api.Tests.Configuration
{
    [CollectionDefinition(nameof(IntegrationApiTestsFixtureCollection))]
    public class IntegrationApiTestsFixtureCollection : ICollectionFixture<IntegrationTestsFixture<Program>> { }

    public class IntegrationTestsFixture<TProgram> : IDisposable where TProgram : class
    {
        public readonly EducacaoOnlineAppFactory<TProgram> Factory;
        public HttpClient Client;

        public required string UsuarioEmail;
        public required string UsuarioSenha;
        public required string UsuarioToken;

        public IntegrationTestsFixture()
        {
            Factory = new EducacaoOnlineAppFactory<TProgram>();
            Client = Factory.CreateClient();
        }

        public void GerarSenhaUsuario()
        {
            var faker = new Faker("pt_BR");
            UsuarioEmail = faker.Internet.Email().ToLower();
            UsuarioSenha = faker.Internet.Password(8, false, "", "@1Ab_");
        }

        public async Task FazerLoginAdministrador()
        {
            var dadosUsuario = new LoginUsuarioModel(
                email: "admin@teste.com",
                password: "Teste@123");

            var response = await Client.PostAsJsonAsync("api/v1/autenticacao/login", dadosUsuario);
            response.EnsureSuccessStatusCode();
            UsuarioToken = await response.Content.ReadAsStringAsync();
        }

        public async Task FazerLoginAluno()
        {
            var dadosUsuario = new LoginUsuarioModel(
                email: "teste@teste.com",
                password: "Teste@123");

            var response = await Client.PostAsJsonAsync("api/v1/autenticacao/login", dadosUsuario);
            response.EnsureSuccessStatusCode();
            UsuarioToken = await response.Content.ReadAsStringAsync();
        }

        public void Dispose()
        {
            Client.Dispose();
            Factory.Dispose();
        }
    }
}