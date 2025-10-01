
namespace EducacaoOnline.GestaoDeConteudo.Domain.Exceptions
{
    public class DisponibilizacaoDeCursoInvalidoException : DomainException
    {
        public DisponibilizacaoDeCursoInvalidoException() : base("Curso inválido não pode receber matrícula.")
        {
        }

        public DisponibilizacaoDeCursoInvalidoException(string? message) : base(message)
        {
        }

        public DisponibilizacaoDeCursoInvalidoException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
