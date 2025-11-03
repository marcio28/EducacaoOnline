using EducacaoOnline.Api.Data;
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

            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlite(connectionString));

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