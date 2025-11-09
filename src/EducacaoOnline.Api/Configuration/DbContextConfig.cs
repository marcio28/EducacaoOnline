using EducacaoOnline.Api.Data;
using EducacaoOnline.GestaoConteudo.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace EducacaoOnline.Api.Configuration
{
    public static class DbContextConfig
    {
        public static IServiceCollection AddDbContextConfig(this IServiceCollection services,
                                                                 IConfiguration configuration)
        {
            var connectionString = configuration
                .GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("String de conexão 'DefaultConnection' não encontrada.");

            services.AddDbContext<IdentityContext>(options => options.UseSqlite(connectionString));
            services.AddDbContext<GestaoConteudoContext>(options => options.UseSqlite(connectionString));

            return services;
        }

        public static IApplicationBuilder UseIdentityConfiguration(this IApplicationBuilder app)
        {
            app.UseAuthentication();
            app.UseAuthorization();

            return app;
        }
    }
}