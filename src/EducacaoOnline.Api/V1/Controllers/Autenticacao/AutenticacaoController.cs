using EducacaoOnline.Api.Controllers;
using EducacaoOnline.Api.Models;
using EducacaoOnline.Core.Messages.ApplicationNotifications;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace EducacaoOnline.Api.V1.Controllers.Autenticacao
{
    [AllowAnonymous]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/autenticacao")]
    public class AutenticacaoController : MainController
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly JwtSettings _jwtSettings;

        public AutenticacaoController(
            SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager,
            IOptions<JwtSettings> jwtSettings,
            INotifiable notifiable) : base(notifiable)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _jwtSettings = jwtSettings.Value;
        }

        [HttpPost("registrar")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Registrar(RegistroUsuarioViewModel registroUsuario)
        {
            if (ModelState.IsValid is false) return RespostaCustomizada(ModelState);

            var usuario = new IdentityUser
            {
                UserName = registroUsuario.Email,
                Email = registroUsuario.Email,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(usuario, registroUsuario.Password);

            if (result.Succeeded is false)
            {
                await _signInManager.SignInAsync(usuario, false);
                return Ok(GerarJwt(usuario.Email));
            }

            return Ok();
        }

        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Login(LoginUsuarioViewModel loginUsuario)
        {
            if (ModelState.IsValid is false) return RespostaCustomizada(ModelState);

            var result = await _signInManager.PasswordSignInAsync(
                loginUsuario.Email, 
                loginUsuario.Password, 
                isPersistent: false, 
                lockoutOnFailure: true);

            if (result.Succeeded is false)
            {
                AdicionarErroProcessamento("Usuário ou senha inválidos.");
                return RespostaCustomizada();
            }

            return RespostaCustomizada(HttpStatusCode.OK, GerarJwt(loginUsuario.Email));
        }

        private async Task<string> GerarJwt(string email)
        {
            var usuario = await _userManager.FindByEmailAsync(email);

            if (usuario is null || usuario.Email is null) return string.Empty;

            var declaracoes = await _userManager.GetClaimsAsync(usuario);
            declaracoes.Add(new Claim(JwtRegisteredClaimNames.Sub, usuario.Id));
            declaracoes.Add(new Claim(JwtRegisteredClaimNames.Email, usuario.Email));
            declaracoes.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            declaracoes.Add(new Claim(JwtRegisteredClaimNames.Nbf, ToUnixEpochDate(DateTime.UtcNow).ToString()));
            declaracoes.Add(new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(DateTime.UtcNow).ToString(), ClaimValueTypes.Integer64));

            var papeisDoUsuario = await _userManager.GetRolesAsync(usuario);
            foreach (var papelDoUsuario in papeisDoUsuario)
            {
                declaracoes.Add(new Claim(ClaimTypes.Role, papelDoUsuario));
            }

            var tokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(_jwtSettings.Segredo ?? string.Empty);

            var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(declaracoes),
                Issuer = _jwtSettings.Emissor,
                Audience = _jwtSettings.Audiencia,
                Expires = DateTime.UtcNow.AddHours(_jwtSettings.ExpiracaoEmHoras),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            });

            return tokenHandler.WriteToken(token);
        }

        private static long ToUnixEpochDate(DateTime data)
            => (long)Math.Round((data.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);
    }
}