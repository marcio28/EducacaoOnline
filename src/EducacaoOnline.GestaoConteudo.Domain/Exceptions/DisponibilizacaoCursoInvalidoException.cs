
using EducacaoOnline.Core.DomainObjects;

namespace EducacaoOnline.GestaoConteudo.Domain.Exceptions
{
    public class DisponibilizacaoCursoInvalidoException : DomainException
    {
        public DisponibilizacaoCursoInvalidoException() : base("Curso inválido não pode receber matrícula.") { }
    }
}
