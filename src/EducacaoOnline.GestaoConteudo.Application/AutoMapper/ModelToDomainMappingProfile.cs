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
                .ConstructUsing(ctor => new Curso(ctor.Id))
                .ForMember(d => d.ConteudoProgramatico, o => o.MapFrom(s => s.Descricao));
        }
    }
}