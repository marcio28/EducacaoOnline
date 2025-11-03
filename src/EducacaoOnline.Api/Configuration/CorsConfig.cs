namespace EducacaoOnline.Api.Configuration
{
    public static class CorsConfig
    {
        public static IServiceCollection AddCorsConfig(this IServiceCollection services, IConfiguration configuration)
        {
            string[] corsOrigins = configuration.GetSection("CorsOrigins").Get<string[]>() ?? [];

            if (corsOrigins.Length == 0)
            {
                services.AddCors(option =>
                {
                    option.AddPolicy("CorsPolicy", policy =>
                    {
                        policy
                         .WithHeaders("Origin", "X-Requested-With", "x-xsrf-token", "Content-Type", "Accept", "Authorization")
                         .WithOrigins(corsOrigins)
                         .AllowAnyMethod()
                         .AllowAnyHeader()
                         .AllowCredentials()
                         .SetIsOriginAllowedToAllowWildcardSubdomains()
                         .SetPreflightMaxAge(TimeSpan.FromDays(10))
                         .Build();
                    });
                });
            }

            return services;
        }
    }
}