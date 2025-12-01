using EducacaoOnline.Api.Data;
using EducacaoOnline.GestaoAlunos.Data.Context;
using EducacaoOnline.GestaoConteudo.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace EducacaoOnline.Api.Configuration
{
    public static class DbContextConfig
    {
        public static IServiceCollection AddDbContextConfig(this IServiceCollection services,
                                                                 IConfiguration configuration)
        {
            var connectionString = configuration
                .GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("String de conexão 'DefaultConnection' não encontrada.");

            if (configuration.GetValue<string>("ASPNETCORE_ENVIRONMENT") == "Production")
            {
                AdicionarContextosUsandoSqlServer(services, connectionString);
            }
            else
            {
                AdicionarContextosUsandoSqlite(services, connectionString);
            }

            return services;
        }

        public static IApplicationBuilder UseIdentityConfiguration(this IApplicationBuilder app)
        {
            app.UseAuthentication();
            app.UseAuthorization();

            return app;
        }

        [ExcludeFromCodeCoverage]
        private static void AdicionarContextosUsandoSqlServer(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<IdentityContext>(
                options => options.UseSqlServer(connectionString, a => a.EnableRetryOnFailure(
                    maxRetryCount: 2,
                    maxRetryDelay: TimeSpan.FromSeconds(5),
                    errorNumbersToAdd: null)));
            services.AddDbContext<GestaoAlunosContext>(
                options => options.UseSqlServer(connectionString, a => a.EnableRetryOnFailure(
                    maxRetryCount: 2,
                    maxRetryDelay: TimeSpan.FromSeconds(5),
                    errorNumbersToAdd: null)));
            services.AddDbContext<GestaoConteudoContext>(
                options => options.UseSqlServer(connectionString, a => a.EnableRetryOnFailure(
                    maxRetryCount: 2,
                    maxRetryDelay: TimeSpan.FromSeconds(5),
                    errorNumbersToAdd: null)));
        }

        private static void AdicionarContextosUsandoSqlite(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<IdentityContext>(options => options.UseSqlite(connectionString));
            services.AddDbContext<GestaoAlunosContext>(options => options.UseSqlite(connectionString));
            services.AddDbContext<GestaoConteudoContext>(options => options.UseSqlite(connectionString));
        }
    }
}