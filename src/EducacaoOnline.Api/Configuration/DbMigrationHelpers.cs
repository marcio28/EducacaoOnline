using EducacaoOnline.Api.Data;
using EducacaoOnline.GestaoConteudo.Data.Context;
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

            var context = scope.ServiceProvider.GetRequiredService<IdentityContext>();

            if (env.IsDevelopment() || env.IsEnvironment("Docker") || env.IsEnvironment("Testing") || env.IsStaging())
            {
                await context.Database.MigrateAsync();
                await EnsureSeedDataIdentity(context);
            }
        }

        private static async Task EnsureSeedDataIdentity(IdentityContext context)
        {
            if (await context.Users.AnyAsync()) return;

            var idAdminUser = Guid.NewGuid();
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
            await context.Users.AddAsync(adminUser);

            var claimsAdmin = new List<IdentityUserClaim<string>>
            { 
                new() {
                    UserId = idAdminUser.ToString(),
                    ClaimType = "Cursos",
                    ClaimValue = "VISUALIZAR,INCLUIR,ALTERAR,EXCLUIR"
                }
            };
            await context.UserClaims.AddRangeAsync(claimsAdmin);
            
            await context.SaveChangesAsync();
        }
    }
}