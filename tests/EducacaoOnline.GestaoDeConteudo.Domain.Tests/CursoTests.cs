using EducacaoOnline.GestaoDeConteudo.Domain.Exceptions;

namespace EducacaoOnline.GestaoDeConteudo.Domain.Tests
{
    public class CursoTests
    {
        [Fact(DisplayName = "Curso Novo Sem Regras Violadas É Válido")]
        [Trait("Categoria", "Gestão de Conteúdo - Curso")]
        public void CursoNovo_SemRegrasVioladas_EhValido()
        {
            // Arrange & Act
            var curso = new Curso(nome: "Curso de C#",
                                  conteudoProgramatico: new ConteudoProgramatico("Conteúdo Programático do Curso de C#"));

            // Assert
            Assert.True(curso.EhValido());
            Assert.Equal(0, curso.ValidationResult?.Errors.Count);
        }

        [Fact(DisplayName = "Curso Novo Com Regras Violadas É Inválido")]
        [Trait("Categoria", "Gestão de Conteúdo - Curso")]
        public void CursoNovo_ComRegrasVioladas_EhInvalido()
        {
            // Arrange && Act
            var curso = new Curso(nome: "A",
                                  conteudoProgramatico: new ConteudoProgramatico("A"));

            // Assert
            Assert.False(curso.EhValido());
            Assert.Equal(2, curso.ValidationResult?.Errors.Count);
        }

        [Fact(DisplayName = "Disponibilizar Curso Válido Recebe Matrícula")]
        [Trait("Categoria", "Gestão de Conteúdo - Curso")]
        public void Disponibilizar_CursoValido_RecebeMatricula()
        {
            // Arrange
            var curso = new Curso(nome: "Curso de C#",
                                  conteudoProgramatico: new ConteudoProgramatico("Conteúdo Programático do Curso de C#"));

            // Act
            curso.DisponibilizarMatricula();

            // Assert
            Assert.True(curso.DisponivelParaMatricula);
        }

        [Fact(DisplayName = "Disponibilizar Curso Inválido Lança Exceção")]
        [Trait("Categoria", "Gestão de Conteúdo - Curso")]
        public void Disponibilizar_CursoInvalido_LancaExcecao()
        {
            // Arrange
            var curso = new Curso(nome: "A",
                                  conteudoProgramatico: new ConteudoProgramatico("A"));

            // Act & Assert
            var exception = Assert.Throws<DisponibilizacaoDeCursoInvalidoException>(() => curso.DisponibilizarMatricula());
            Assert.False(curso.DisponivelParaMatricula);
        }

        [Fact(DisplayName = "Adicionar Aula Válida")]
        [Trait("Categoria", "Gestão de Conteúdo - Curso")]
        public void AdicionarAula_Valida()
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


        [Fact(DisplayName = "Adicionar Aula Inválida Lança Exceção")]
        [Trait("Categoria", "Gestão de Conteúdo - Curso")]
        public void AdicionarAula_Invalida_LancaExcecao()
        {
            // Arrange
            var curso = new Curso(nome: "Curso de C#",
                                  conteudoProgramatico: new ConteudoProgramatico("Conteúdo Programático do Curso de C#"));
            var quantidadeAulasAntes = curso.Aulas?.Count ?? 0;
            var tituloDaAula = "I";

            // Act && Assert
            var excecao = Assert.Throws<DomainException>(() => curso.AdicionarAula(tituloDaAula, conteudo: "A"));

            var regrasVioladas = excecao.RegrasVioladas;
            Assert.NotNull(regrasVioladas);
            Assert.Equal(2, regrasVioladas.Length);

            var quantidadeAulasDepois = curso.Aulas?.Count ?? 0;
            Assert.Equal(quantidadeAulasAntes, quantidadeAulasDepois);

            Assert.DoesNotContain(curso.Aulas ?? [], a => a.Titulo.Equals(tituloDaAula, StringComparison.OrdinalIgnoreCase));
        }
    }
}