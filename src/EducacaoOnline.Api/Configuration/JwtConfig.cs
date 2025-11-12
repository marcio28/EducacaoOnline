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

            var jwtSettings = JwtSettingsSection.Get<JwtSettings>();

            var key = Encoding.ASCII.GetBytes(jwtSettings?.Segredo ?? string.Empty);

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
                    ValidAudience = jwtSettings?.Audiencia ?? string.Empty,
                    ValidIssuer = jwtSettings?.Emissor ?? string.Empty,
                };
            });

            return services;
        }
    }
}