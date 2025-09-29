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
            var curso = new Curso(nome, conteudoProgramatico);

            // Act
            var result = curso.EhValido();

            // Assert
            Assert.True(result);
        }
    }
}