using AutoMapper;
using EducacaoOnline.GestaoConteudo.Application.Models;
using EducacaoOnline.GestaoConteudo.Domain;

namespace EducacaoOnline.GestaoConteudo.Application.AutoMapper
{
    public class DomainToModelMappingProfile : Profile
    {
        public DomainToModelMappingProfile()
        {
            CreateMap<Curso, CursoModel>()
                .ForMember(d => d.Id, o => o.MapFrom(s => s.Id))
                .ForMember(d => d.Descricao, o => o.MapFrom(s => s.ConteudoProgramatico.Descricao));
        }
    }
}