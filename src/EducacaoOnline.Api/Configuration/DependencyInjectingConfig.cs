using EducacaoOnline.Core.Messages.DomainNotifications;
using EducacaoOnline.GestaoAlunos.Data.Repositories;
using EducacaoOnline.GestaoAlunos.Domain.Repositories;
using EducacaoOnline.GestaoConteudo.Data.Repositories;
using EducacaoOnline.GestaoConteudo.Domain.Repositories;
using EducacaoOnline.GestaoConteudo.Domain.Services;
using MediatR;

namespace EducacaoOnline.Api.Configuration
{
    public static class DependencyInjectingConfig
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            // Notificações
            services.AddScoped<INotificationHandler<NotificacaoDominio>, NotificacaoDominioHandler>();

            // Repositórios
            services.AddScoped<IAlunoRepository, AlunoRepository>();
            services.AddScoped<ICursoRepository, CursoRepository>();

            // Serviços
            services.AddScoped<ICursoService, CursoService>();

            return services;
        }
    }
}