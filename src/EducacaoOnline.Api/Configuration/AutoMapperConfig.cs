using EducacaoOnline.GestaoConteudo.Application.AutoMapper;

namespace EducacaoOnline.Api.Configuration
{
    public static class AutoMapperAdd
    {
        public static IServiceCollection AddAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(
                typeof(DomainToModelMappingProfile), 
                typeof(ModelToDomainMappingProfile));

            return services;
        }
    }
}