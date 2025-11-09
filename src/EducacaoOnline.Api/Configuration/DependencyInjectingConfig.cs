using EducacaoOnline.Core.Messages.ApplicationNotifications;
using EducacaoOnline.GestaoConteudo.Data.Repositories;
using EducacaoOnline.GestaoConteudo.Domain.Repositories;
using EducacaoOnline.GestaoConteudo.Domain.Services;

namespace EducacaoOnline.Api.Configuration
{
    public static class DependencyInjectingConfig
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            // Repositories
            services.AddScoped<ICursoRepository, CursoRepository>();

            //Notification
            services.AddScoped<INotifiable, Notifiable>();

            //Services
            services.AddScoped<ICursoService, CursoService>();

            return services;
        }
    }
}