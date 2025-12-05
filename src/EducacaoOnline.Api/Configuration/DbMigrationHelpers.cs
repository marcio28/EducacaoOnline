using EducacaoOnline.Api.Data;
using EducacaoOnline.GestaoAlunos.Data;
using EducacaoOnline.GestaoAlunos.Domain;
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
                var gestaoAlunosContext = scope.ServiceProvider.GetRequiredService<GestaoAlunosContext>();
                var gestaoConteudoContext = scope.ServiceProvider.GetRequiredService<GestaoConteudoContext>();

                // Aplica as migrações pendentes. Cria o banco de dados se ele não existir.
                await identityContext.Database.MigrateAsync();
                await gestaoAlunosContext.Database.MigrateAsync();
                await gestaoConteudoContext.Database.MigrateAsync();

                // Insere os dados iniciais de teste
                var idUsuarioAdministrador = Guid.NewGuid();
                var idUsuarioAluno = Guid.NewGuid();
                await EnsureSeedDataIdentity(identityContext, idUsuarioAdministrador, idUsuarioAluno);
                await EnsureSeedDataGestaoAlunos(gestaoAlunosContext, idUsuarioAluno);
                await EnsureSeedDataGestaoConteudo(gestaoConteudoContext, idUsuarioAdministrador);
            }
        }

        private static async Task EnsureSeedDataIdentity(IdentityContext identityContext, Guid idUsuarioAdministrador, Guid idUsuarioAluno)
        {
            if (await identityContext.Users.AnyAsync()) return;

            var usuarioAdministrador = new IdentityUser
            {
                Id = idUsuarioAdministrador.ToString(),
                Email = "admin@teste.com",
                EmailConfirmed = true,
                NormalizedEmail = "ADMIN@TESTE.COM",
                UserName = "admin@teste.com",
                AccessFailedCount = 0,
                PasswordHash = "AQAAAAIAAYagAAAAEF/nmfwFGPa8pnY9AvZL8HKI7r7l+aM4nryRB+Y3Ktgo6d5/0d25U2mhixnO4h/K5w==", // Teste@123
                NormalizedUserName = "ADMIN@TESTE.COM"
            };
            await identityContext.Users.AddAsync(usuarioAdministrador);

            var declaracoesAdministrador = new List<IdentityUserClaim<string>>
            { 
                new() {
                    UserId = idUsuarioAdministrador.ToString(),
                    ClaimType = "Cursos",
                    ClaimValue = "Cadastrar"
                },
                new() {
                    UserId = idUsuarioAdministrador.ToString(),
                    ClaimType = "Aulas",
                    ClaimValue = "Cadastrar"
                },
                new() {
                    UserId = idUsuarioAdministrador.ToString(),
                    ClaimType = "Matriculas",
                    ClaimValue = "Gerir"
                },
                new() {
                    UserId = idUsuarioAdministrador.ToString(),
                    ClaimType = "Pagamentos",
                    ClaimValue = "Gerir"
                },
                new() {
                    UserId = idUsuarioAdministrador.ToString(),
                    ClaimType = "Alunos",
                    ClaimValue = "Gerir"
                }
            };
            await identityContext.UserClaims.AddRangeAsync(declaracoesAdministrador);

            var usuarioAluno = new IdentityUser
            {
                Id = idUsuarioAluno.ToString(),
                Email = "aluno@teste.com",
                EmailConfirmed = true,
                NormalizedEmail = "ALUNO@TESTE.COM",
                UserName = "aluno@teste.com",
                AccessFailedCount = 0,
                PasswordHash = "AQAAAAIAAYagAAAAEF/nmfwFGPa8pnY9AvZL8HKI7r7l+aM4nryRB+Y3Ktgo6d5/0d25U2mhixnO4h/K5w==", // Teste@123
                NormalizedUserName = "ALUNO@TESTE.COM"
            };
            await identityContext.Users.AddAsync(usuarioAluno);

            var declaracoesAluno = new List<IdentityUserClaim<string>>
            {
                new() {
                    UserId = idUsuarioAluno.ToString(),
                    ClaimType = "Cursos",
                    ClaimValue = "MatricularSe,Finalizar"
                },
                new() {
                    UserId = idUsuarioAluno.ToString(),
                    ClaimType = "Aulas",
                    ClaimValue = "Assistir"
                },
                new() {
                    UserId = idUsuarioAluno.ToString(),
                    ClaimType = "MeusDados",
                    ClaimValue = "Gerir"
                },
                new() {
                    UserId = idUsuarioAluno.ToString(),
                    ClaimType = "MinhasMatriculas",
                    ClaimValue = "Gerir"
                },
                new() {
                    UserId = idUsuarioAluno.ToString(),
                    ClaimType = "MeusCertificados",
                    ClaimValue = "Gerir"
                }
            };
            await identityContext.UserClaims.AddRangeAsync(declaracoesAluno);

            await identityContext.SaveChangesAsync();
        }

        private static async Task EnsureSeedDataGestaoAlunos(GestaoAlunosContext gestaoAlunosContext, Guid idUsuarioAluno)
        {
            if (await gestaoAlunosContext.Alunos.AnyAsync()) return;

            var aluno = new Aluno(idUsuarioAluno);

            await gestaoAlunosContext.Alunos.AddAsync(aluno);

            await gestaoAlunosContext.SaveChangesAsync();
        }

        private static async Task EnsureSeedDataGestaoConteudo(GestaoConteudoContext gestaoConteudoContext, Guid idUsuarioAdministrador)
        {
            if (await gestaoConteudoContext.Cursos.AnyAsync()) return;

            var curso = new Curso(
                nome: "Curso Seed 1",
                conteudoProgramatico: new ConteudoProgramatico("Conteúdo Programático do Curso Seed 1"));

            await gestaoConteudoContext.Cursos.AddAsync(curso);

            var administrador = new Administrador(idUsuarioAdministrador);

            await gestaoConteudoContext.Administradores.AddAsync(administrador);

            await gestaoConteudoContext.SaveChangesAsync();
        }
    }
}