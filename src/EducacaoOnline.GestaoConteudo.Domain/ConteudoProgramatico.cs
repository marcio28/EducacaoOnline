
using EducacaoOnline.Core.DomainObjects;

namespace EducacaoOnline.GestaoConteudo.Domain
{
    public class ConteudoProgramatico(string descricao) : ValueObject
    {
        public string Descricao { get; private set; } = descricao;
    }
}