using EducacaoOnline.Api.Tests.Config;

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
            _testsFixture.GerarSenhaUsuario();

            var requestBody = new Dictionary<string, string>
            {
                {"email", _testsFixture.UsuarioEmail },
                {"password", _testsFixture.UsuarioSenha },
                {"confirmPassword", _testsFixture.UsuarioSenha }
            };

            var postRequest = new HttpRequestMessage(HttpMethod.Post, URIRegistro)
            {
                Content = new FormUrlEncodedContent(requestBody)
            };

            // Act
            var postResponse = await _testsFixture.Client.SendAsync(postRequest);

            // Assert
            var responseString = await postResponse.Content.ReadAsStringAsync();

            postResponse.EnsureSuccessStatusCode();
            Assert.Contains($"Hello {_testsFixture.UsuarioEmail}!", responseString);
        }

        [Fact(DisplayName = "Registrar usuário com senha fraca"), TestPriority(3)]
        [Trait("Categoria", "Integração API - Usuário")]
        public async Task Usuario_RealizarCadastroComSenhaFraca_DeveRetornarMensagemDeErro()
        {
            // Arrange
            var initialResponse = await _testsFixture.Client.GetAsync(URIRegistro);
            initialResponse.EnsureSuccessStatusCode();

            var antiForgeryToken = _testsFixture.ObterAntiForgeryToken(await initialResponse.Content.ReadAsStringAsync());

            _testsFixture.GerarSenhaUsuario();
            const string senhaFraca = "123456";

            var formData = new Dictionary<string, string>
            {
                { _testsFixture.AntiForgeryFieldName, antiForgeryToken },
                {"Input.Email", _testsFixture.UsuarioEmail },
                {"Input.Password", senhaFraca },
                {"Input.ConfirmPassword", senhaFraca }
            };

            var postRequest = new HttpRequestMessage(HttpMethod.Post, URIRegistro)
            {
                Content = new FormUrlEncodedContent(formData)
            };

            // Act
            var postResponse = await _testsFixture.Client.SendAsync(postRequest);

            // Assert
            var responseString = await postResponse.Content.ReadAsStringAsync();

            postResponse.EnsureSuccessStatusCode();
            Assert.Contains("Passwords must have at least one non alphanumeric character.", responseString);
            Assert.Contains("Passwords must have at least one lowercase (&#x27;a&#x27;-&#x27;z&#x27;).", responseString);
            Assert.Contains("Passwords must have at least one uppercase (&#x27;A&#x27;-&#x27;Z&#x27;).", responseString);
        }

        [Fact(DisplayName = "Fazer login com sucesso"), TestPriority(2)]
        [Trait("Categoria", "Integração API - Usuário")]
        public async Task Usuario_RealizarLogin_DeveExecutarComSucesso()
        {
            // Arrange
            var initialResponse = await _testsFixture.Client.GetAsync(URILogin);
            initialResponse.EnsureSuccessStatusCode();

            var antiForgeryToken = _testsFixture.ObterAntiForgeryToken(await initialResponse.Content.ReadAsStringAsync());

            var formData = new Dictionary<string, string>
            {
                {_testsFixture.AntiForgeryFieldName, antiForgeryToken},
                {"Input.Email", _testsFixture.UsuarioEmail},
                {"Input.Password", _testsFixture.UsuarioSenha}
            };

            var postRequest = new HttpRequestMessage(HttpMethod.Post, URILogin)
            {
                Content = new FormUrlEncodedContent(formData)
            };

            // Act
            var postResponse = await _testsFixture.Client.SendAsync(postRequest);

            // Assert
            var responseString = await postResponse.Content.ReadAsStringAsync();

            postResponse.EnsureSuccessStatusCode();
            Assert.Contains($"Hello {_testsFixture.UsuarioEmail}!", responseString);
        }
    }
}