using EducacaoOnline.Core.DomainObjects;

namespace EducacaoOnline.GestaoAlunos.Domain.Exceptions
{
    public class MatriculaCursoInvalidoException : DomainException
    {
        public MatriculaCursoInvalidoException() : base("Curso inválido.") { }

    }
}