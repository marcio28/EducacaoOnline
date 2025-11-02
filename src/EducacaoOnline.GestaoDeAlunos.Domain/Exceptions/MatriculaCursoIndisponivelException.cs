using EducacaoOnline.Core.DomainObjects;

namespace EducacaoOnline.GestaoDeAlunos.Domain.Exceptions
{
    public class MatriculaCursoIndisponivelException : DomainException
    {
        public MatriculaCursoIndisponivelException() : base("Curso indisponível para matrícula.") { }

    }
}