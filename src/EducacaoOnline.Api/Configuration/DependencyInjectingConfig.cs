using EducacaoOnline.Core.Messages.ApplicationNotifications;

namespace EducacaoOnline.Api.Configuration
{
    public static class DependencyInjectingConfig
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            // Repositories

            //Notification
            services.AddScoped<INotifiable, Notifiable>();

            //Services

            return services;
        }
    }
}