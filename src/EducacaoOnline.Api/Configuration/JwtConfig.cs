using EducacaoOnline.Api.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace EducacaoOnline.Api.Configuration
{
    public static class JwtConfig
    {
        public static IServiceCollection AddJwtConfig(this IServiceCollection services,
                                                           IConfiguration configuration)
        {
            var JwtSettingsSection = configuration.GetSection("JwtSettings");

            services.Configure<JwtSettings>(JwtSettingsSection);

            var jwtSettings = JwtSettingsSection.Get<JwtSettings>() ?? throw new InvalidOperationException("Configuração de JWT inválida.");

            if (string.IsNullOrEmpty(jwtSettings.Segredo))
                throw new InvalidOperationException("O segredo do JWT não pode ser nulo ou vazio.");

            if (string.IsNullOrEmpty(jwtSettings.Emissor))
                throw new InvalidOperationException("O emissor do JWT não pode ser nulo ou vazio.");

            if (string.IsNullOrEmpty(jwtSettings.Audiencia))
                throw new InvalidOperationException("A audiência do JWT não pode ser nulo ou vazio.");

            var key = Encoding.ASCII.GetBytes(jwtSettings.Segredo);

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = true;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = jwtSettings.Audiencia,
                    ValidIssuer = jwtSettings.Emissor,
                };
            });

            return services;
        }
    }
}