using Microsoft.AspNetCore.HttpOverrides;
using System.Text.Json.Serialization;

namespace EducacaoOnline.Api.Configuration
{
    public static class ApiConfig
    {
        public static IServiceCollection AddApiConfig(this IServiceCollection services)
        {
            services.AddControllers()
                .ConfigureApiBehaviorOptions(action => action.SuppressModelStateInvalidFilter = true)
                .AddJsonOptions(configure =>
                {
                    configure.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                    configure.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                });
            services.Configure<ForwardedHeadersOptions>(options =>
            {
                options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
            });

            return services;
        }

        public static IApplicationBuilder UseApiConfiguration(this IApplicationBuilder app, IWebHostEnvironment environment)
        {
            app.UseForwardedHeaders();

            app.UseCors("CorsPolicy");

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseIdentityConfiguration();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            return app;
        }
    }
}