namespace EducacaoOnline.GestaoDeConteudo.Domain.Tests
{
    public class CursoTests
    {
        [Fact(DisplayName = "Curso Novo Válido")]
        [Trait("Categoria", "Gestão de Conteúdo - Curso")]
        public void CadastrarCurso_CursoNovo_DeveSerValido()
        {
            // Arrange
            var nome = "Curso de C#";
            var conteudoProgramatico = new ConteudoProgramatico("Conteúdo Programático do Curso de C#");
            var curso = new Curso(nome, conteudoProgramatico);

            // Act
            var result = curso.EhValido();

            // Assert
            Assert.True(result);
        }
    }
}