using EducacaoOnline.Core.DomainObjects;

namespace EducacaoOnline.GestaoAlunos.Domain.Exceptions
{
    public class MatriculaCursoIndisponivelException : DomainException
    {
        public MatriculaCursoIndisponivelException() : base("Curso indisponível para matrícula.") { }

    }
}