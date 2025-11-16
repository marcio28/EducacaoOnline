using EducacaoOnline.Api.Data;
using EducacaoOnline.GestaoConteudo.Data.Context;
using EducacaoOnline.GestaoConteudo.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EducacaoOnline.Api.Configuration
{
    public static class DbMigrationHelperExtension
    {
        public static void UseDbMigrationHelper(this WebApplication app)
        {
            DbMigrationHelper.EnsureSeedData(app).Wait();
        }
    }

    public static class DbMigrationHelper
    {
        public static async Task EnsureSeedData(WebApplication serviceScope)
        {
            var services = serviceScope.Services.CreateScope().ServiceProvider;
            await EnsureSeedData(services);
        }

        public static async Task EnsureSeedData(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope();
            var env = scope.ServiceProvider.GetRequiredService<IWebHostEnvironment>();


            if (env.IsDevelopment() || env.IsEnvironment("Docker") || env.IsEnvironment("Testing") || env.IsStaging())
            {
                var identityContext = scope.ServiceProvider.GetRequiredService<IdentityContext>();
                var gestaoConteudoContext = scope.ServiceProvider.GetRequiredService<GestaoConteudoContext>();

                var idAdminUser = Guid.NewGuid();

                await identityContext.Database.MigrateAsync();
                await EnsureSeedDataIdentity(identityContext, idAdminUser);

                await gestaoConteudoContext.Database.MigrateAsync();
                await EnsureSeedDataGestaoConteudo(gestaoConteudoContext, idAdminUser);
            }
        }

        private static async Task EnsureSeedDataIdentity(IdentityContext identityContext, Guid idAdminUser)
        {
            if (await identityContext.Users.AnyAsync()) return;

            var adminUser = new IdentityUser
            {
                Id = idAdminUser.ToString(),
                Email = "admin@teste.com",
                EmailConfirmed = true,
                NormalizedEmail = "ADMIN@TESTE.COM",
                UserName = "admin@teste.com",
                AccessFailedCount = 0,
                PasswordHash = "AQAAAAIAAYagAAAAEF/nmfwFGPa8pnY9AvZL8HKI7r7l+aM4nryRB+Y3Ktgo6d5/0d25U2mhixnO4h/K5w==", // Teste@123
                NormalizedUserName = "ADMIN@TESTE.COM"
            };
            await identityContext.Users.AddAsync(adminUser);

            var claimsAdmin = new List<IdentityUserClaim<string>>
            { 
                new() {
                    UserId = idAdminUser.ToString(),
                    ClaimType = "Cursos",
                    ClaimValue = "VISUALIZAR,INCLUIR,ALTERAR,EXCLUIR"
                }
            };
            await identityContext.UserClaims.AddRangeAsync(claimsAdmin);
            
            await identityContext.SaveChangesAsync();
        }

        private static async Task EnsureSeedDataGestaoConteudo(GestaoConteudoContext gestaoConteudoContext, Guid idAdminUser)
        {
            if (await gestaoConteudoContext.Cursos.AnyAsync()) return;

            var curso = new Curso(
                nome: "Curso Seed 1",
                conteudoProgramatico: new ConteudoProgramatico("Conteúdo Programático do Curso Seed 1"));

            await gestaoConteudoContext.Cursos.AddAsync(curso);

            var administrador = new Administrador(idAdminUser);

            await gestaoConteudoContext.Administradores.AddAsync(administrador);

            await gestaoConteudoContext.SaveChangesAsync();
        }

    }
}