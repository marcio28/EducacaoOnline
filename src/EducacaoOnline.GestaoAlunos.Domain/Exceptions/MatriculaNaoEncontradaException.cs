using EducacaoOnline.Core.DomainObjects;

namespace EducacaoOnline.GestaoAlunos.Domain.Exceptions
{
    public class MatriculaNaoEncontradaException : DomainException
    {
        public MatriculaNaoEncontradaException() : base("Matrícula não encontrada.") { }
    }
}