using EducacaoOnline.GestaoDeConteudo.Domain.Exceptions;

namespace EducacaoOnline.GestaoDeConteudo.Domain.Tests
{
    public class CursoTests
    {
        [Fact(DisplayName = "Curso Novo V�lido")]
        [Trait("Categoria", "Gest�o de Conte�do - Curso")]
        public void CadastrarCurso_CursoNovo_DeveSerValido()
        {
            // Arrange
            var nome = "Curso de C#";
            var conteudoProgramatico = new ConteudoProgramatico("Conte�do Program�tico do Curso de C#");

            // Act
            var curso = new Curso(nome, conteudoProgramatico);

            // Assert
            Assert.True(curso.EhValido());
            Assert.Equal(0, curso.ValidationResult?.Errors.Count);
        }

        [Fact(DisplayName = "Curso Novo Inv�lido")]
        [Trait("Categoria", "Gest�o de Conte�do - Curso")]
        public void CadastrarCurso_CursoNovo_DeveSerInvalido()
        {
            // Arrange
            var nome = "A";
            var conteudoProgramatico = new ConteudoProgramatico("A");

            // Act
            var curso = new Curso(nome, conteudoProgramatico);

            // Assert
            Assert.False(curso.EhValido());
            Assert.Equal(2, curso.ValidationResult?.Errors.Count);
        }

        [Fact(DisplayName = "Curso V�lido Pode Ser Disponibilizado")]
        [Trait("Categoria", "Gest�o de Conte�do - Curso")]
        public void CadastrarCurso_CursoValido_PodeSerDisponibilizado()
        {
            // Arrange
            var nome = "Curso de C#";
            var conteudoProgramatico = new ConteudoProgramatico("Conte�do Program�tico do Curso de C#");
            var curso = new Curso(nome, conteudoProgramatico);

            // Act
            curso.TornarDisponivelParaMatricula();

            // Assert
            Assert.True(curso.DisponivelParaMatricula);
        }

        [Fact(DisplayName = "Curso Inv�lido N�o Pode Ser Disponibilizado")]
        [Trait("Categoria", "Gest�o de Conte�do - Curso")]
        public void CadastrarCurso_CursoInvalido_NaoPodeSerDisponibilizado()
        {
            // Arrange
            var nome = "A";
            var conteudoProgramatico = new ConteudoProgramatico("A");
            var curso = new Curso(nome, conteudoProgramatico);

            // Act & Assert
            var exception = Assert.Throws<DisponibilizacaoDeCursoInvalidoException>(() => curso.TornarDisponivelParaMatricula());
            Assert.False(curso.DisponivelParaMatricula);
        }
    }
}