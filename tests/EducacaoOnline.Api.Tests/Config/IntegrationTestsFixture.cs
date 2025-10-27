using Bogus;
using EducacaoOnline.Api.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http.Json;
using System.Text.RegularExpressions;

namespace EducacaoOnline.Api.Tests.Config
{
    [CollectionDefinition(nameof(IntegrationWebTestsFixtureCollection))]
    public class IntegrationWebTestsFixtureCollection : ICollectionFixture<IntegrationTestsFixture<Program>> { }

    [CollectionDefinition(nameof(IntegrationApiTestsFixtureCollection))]
    public class IntegrationApiTestsFixtureCollection : ICollectionFixture<IntegrationTestsFixture<Program>> { }

    public class IntegrationTestsFixture<TProgram> : IDisposable where TProgram : class
    {
        public string AntiForgeryFieldName = "__RequestVerificationToken";

        public readonly EducacaoOnlineAppFactory<TProgram> Factory;
        public HttpClient Client;

        public required string UsuarioEmail;
        public required string UsuarioSenha;
        public required string UsuarioToken;

        public IntegrationTestsFixture()
        {
            var clientOptions = new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = true,
                BaseAddress = new Uri("http://localhost"),
                HandleCookies = true,
                MaxAutomaticRedirections = 7
            };

            Factory = new EducacaoOnlineAppFactory<TProgram>();
            Client = Factory.CreateClient(clientOptions);
        }

        public void GerarSenhaUsuario()
        {
            var faker = new Faker("pt_BR");
            UsuarioEmail = faker.Internet.Email().ToLower();
            UsuarioSenha = faker.Internet.Password(8, false, "", "@1Ab_");
        }

        public async Task FazerLoginApi()
        {
            var dadosUsuario = new LoginUsuarioViewModel
            {
                Email = "teste@teste.com",
                Senha = "Teste@123"
            };

            // Recriando o client para evitar configurações de Web
            Client = Factory.CreateClient();

            var response = await Client.PostAsJsonAsync("api/login", dadosUsuario);
            response.EnsureSuccessStatusCode();
            UsuarioToken = await response.Content.ReadAsStringAsync();
        }

        public string ObterAntiForgeryToken(string htmlBody)
        {
            var requestVerificationTokenMatch =
                Regex.Match(htmlBody, $@"\<input name=""{AntiForgeryFieldName}"" type=""hidden"" value=""([^""]+)"" \/\>");

            if (requestVerificationTokenMatch.Success)
            {
                return requestVerificationTokenMatch.Groups[1].Captures[0].Value;
            }

            throw new ArgumentException($"Anti forgery token '{AntiForgeryFieldName}' não encontrado no HTML", nameof(htmlBody));
        }

        public void Dispose()
        {
            Client.Dispose();
            Factory.Dispose();
        }
    }
}