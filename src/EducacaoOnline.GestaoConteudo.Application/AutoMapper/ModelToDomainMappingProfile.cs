using AutoMapper;
using EducacaoOnline.GestaoConteudo.Application.Models;
using EducacaoOnline.GestaoConteudo.Domain;

namespace EducacaoOnline.GestaoConteudo.Application.AutoMapper
{
    public class ModelToDomainMappingProfile : Profile
    {
        public ModelToDomainMappingProfile()
        {
            CreateMap<CursoModel, Curso>()
                .ForMember(d => d.ConteudoProgramatico, o => o.MapFrom(s => s.Descricao));
        }
    }
}