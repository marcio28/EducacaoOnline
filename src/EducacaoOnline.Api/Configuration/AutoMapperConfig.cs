using AutoMapper;
using EducacaoOnline.Api.Models;
using EducacaoOnline.GestaoConteudo.Domain;

namespace EducacaoOnline.Api.Configuration
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<Curso, CursoModel>().ForMember(dest => dest.Descricao, 
                opt => opt.MapFrom(src => src.ConteudoProgramatico.Descricao));

            CreateMap<CursoModel, Curso>().ForMember(dest => dest.ConteudoProgramatico,
                opt => opt.MapFrom(src => src.Descricao));
        }
    }

    public static class AutoMapperAdd
    {
        public static IServiceCollection AddAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            return services;
        }
    }
}