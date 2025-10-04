using EducacaoOnline.GestaoDeConteudo.Domain.Exceptions;

namespace EducacaoOnline.GestaoDeConteudo.Domain.Tests
{
    public class CursoTests
    {
        [Fact(DisplayName = "Curso Novo Válido")]
        [Trait("Categoria", "Gestão de Conteúdo - Curso")]
        public void CadastrarCurso_CursoNovo_DeveSerValido()
        {
            // Arrange & Act
            var curso = new Curso(nome: "Curso de C#",
                                  conteudoProgramatico: new ConteudoProgramatico("Conteúdo Programático do Curso de C#"));

            // Assert
            Assert.True(curso.EhValido());
            Assert.Equal(0, curso.ValidationResult?.Errors.Count);
        }

        [Fact(DisplayName = "Curso Novo Inválido")]
        [Trait("Categoria", "Gestão de Conteúdo - Curso")]
        public void CadastrarCurso_CursoNovo_DeveSerInvalido()
        {
            // Arrange && Act
            var curso = new Curso(nome: "A",
                                  conteudoProgramatico: new ConteudoProgramatico("A"));

            // Assert
            Assert.False(curso.EhValido());
            Assert.Equal(2, curso.ValidationResult?.Errors.Count);
        }

        [Fact(DisplayName = "Curso Válido Pode Ser Disponibilizado")]
        [Trait("Categoria", "Gestão de Conteúdo - Curso")]
        public void CadastrarCurso_CursoValido_PodeSerDisponibilizado()
        {
            // Arrange
            var curso = new Curso(nome: "Curso de C#",
                                  conteudoProgramatico: new ConteudoProgramatico("Conteúdo Programático do Curso de C#"));

            // Act
            curso.TornarDisponivelParaMatricula();

            // Assert
            Assert.True(curso.DisponivelParaMatricula);
        }

        [Fact(DisplayName = "Curso Inválido Não Pode Ser Disponibilizado")]
        [Trait("Categoria", "Gestão de Conteúdo - Curso")]
        public void CadastrarCurso_CursoInvalido_NaoPodeSerDisponibilizado()
        {
            // Arrange
            var curso = new Curso(nome: "A",
                                  conteudoProgramatico: new ConteudoProgramatico("A"));

            // Act & Assert
            var exception = Assert.Throws<DisponibilizacaoDeCursoInvalidoException>(() => curso.TornarDisponivelParaMatricula());
            Assert.False(curso.DisponivelParaMatricula);
        }

        [Fact(DisplayName = "Adicionar Aula Válida")]
        [Trait("Categoria", "Gestão de Conteúdo - Curso")]
        public void AdicionarAula_AulaNova_DeveEntrarNaColecao()
        {
            // Arrange
            var curso = new Curso(nome: "Curso de C#",
                                  conteudoProgramatico: new ConteudoProgramatico("Conteúdo Programático do Curso de C#"));
            var quantidadeAulasAntes = curso.Aulas?.Count ?? 0;
            var tituloDaAula = "Introdução";

            // Act
            curso.AdicionarAula(tituloDaAula, 
                                conteudo: "Apresentação do curso, do professor e dos objetivos do curso.");

            // Assert
            var quantidadeAulasDepois = curso.Aulas?.Count ?? 0;
            Assert.Equal(quantidadeAulasAntes + 1, quantidadeAulasDepois);
            Assert.Contains(curso.Aulas ?? [], a => a.Titulo.Equals(tituloDaAula, StringComparison.OrdinalIgnoreCase));
        }


        [Fact(DisplayName = "Adicionar Aula Inválida")]
        [Trait("Categoria", "Gestão de Conteúdo - Curso")]
        public void AdicionarAula_AulaInvalida_NaoDeveEntrarNaColecao()
        {
            // Arrange
            var curso = new Curso(nome: "Curso de C#",
                                  conteudoProgramatico: new ConteudoProgramatico("Conteúdo Programático do Curso de C#"));
            var quantidadeAulasAntes = curso.Aulas?.Count ?? 0;
            var tituloDaAula = "I";

            // Act && Assert
            Assert.Throws<DomainException>(() => curso.AdicionarAula(tituloDaAula, conteudo: "A"));
            var quantidadeAulasDepois = curso.Aulas?.Count ?? 0;
            Assert.Equal(quantidadeAulasAntes, quantidadeAulasDepois);
            Assert.DoesNotContain(curso.Aulas ?? [], a => a.Titulo.Equals(tituloDaAula, StringComparison.OrdinalIgnoreCase));
        }
    }
}