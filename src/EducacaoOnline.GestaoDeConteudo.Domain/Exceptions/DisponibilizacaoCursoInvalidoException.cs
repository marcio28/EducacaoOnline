
namespace EducacaoOnline.GestaoDeConteudo.Domain.Exceptions
{
    public class DisponibilizacaoCursoInvalidoException : DomainException
    {
        public DisponibilizacaoCursoInvalidoException() : base("Curso inválido não pode receber matrícula.")
        {
        }

        public DisponibilizacaoCursoInvalidoException(string? message) : base(message)
        {
        }

        public DisponibilizacaoCursoInvalidoException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
