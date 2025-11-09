using EducacaoOnline.Api.Data;
using Microsoft.AspNetCore.Identity;

namespace EducacaoOnline.Api.Configuration
{
    public static class IdentityConfig
    {
        public static IServiceCollection AddIdentityConfig(this IServiceCollection services)
        {
            services.AddIdentity<IdentityUser, IdentityRole>()
                    .AddRoles<IdentityRole>()
                    .AddEntityFrameworkStores<IdentityContext>();

            return services;
        }
    }
}