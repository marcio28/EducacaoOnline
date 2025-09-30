
namespace EducacaoOnline.GestaoDeConteudo.Domain
{
    [Serializable]
    public class CursoInvalidoNaoPodeReceberMatriculaException : Exception
    {
        public CursoInvalidoNaoPodeReceberMatriculaException() : base("Curso inválido não pode receber matrícula.")
        {
        }

        public CursoInvalidoNaoPodeReceberMatriculaException(string? message) : base(message)
        {
        }

        public CursoInvalidoNaoPodeReceberMatriculaException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}