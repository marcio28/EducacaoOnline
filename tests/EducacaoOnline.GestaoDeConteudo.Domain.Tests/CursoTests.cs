using EducacaoOnline.Core.DomainObjects;
using EducacaoOnline.GestaoDeConteudo.Domain.Exceptions;

namespace EducacaoOnline.GestaoDeConteudo.Domain.Tests
{
    public class CursoTests
    {
        [Fact(DisplayName = "Curso Novo Sem Viola��es � V�lido")]
        [Trait("Categoria", "Gest�o de Conte�do - Curso")]
        public void CursoNovo_SemViolacoes_EhValido()
        {
            // Arrange & Act
            var curso = new Curso(nome: "Curso de C#",
                                  conteudoProgramatico: new ConteudoProgramatico("Conte�do Program�tico do Curso de C#"));

            // Assert
            Assert.True(curso.EhValido());
            Assert.Equal(0, curso.ValidationResult?.Errors.Count);
        }

        [Fact(DisplayName = "Curso Novo Com Violacoes � Inv�lido")]
        [Trait("Categoria", "Gest�o de Conte�do - Curso")]
        public void CursoNovo_ComViolacoes_EhInvalido()
        {
            // Arrange && Act
            var curso = new Curso(nome: "A",
                                  conteudoProgramatico: new ConteudoProgramatico("A"));

            // Assert
            Assert.False(curso.EhValido());
            Assert.Equal(2, curso.ValidationResult?.Errors.Count);
        }

        [Fact(DisplayName = "Disponibilizar Curso V�lido Recebe Matr�cula")]
        [Trait("Categoria", "Gest�o de Conte�do - Curso")]
        public void Disponibilizar_CursoValido_RecebeMatricula()
        {
            // Arrange
            var curso = new Curso(nome: "Curso de C#",
                                  conteudoProgramatico: new ConteudoProgramatico("Conte�do Program�tico do Curso de C#"));

            // Act
            curso.DisponibilizarMatricula();

            // Assert
            Assert.True(curso.DisponivelMatricula);
        }

        [Fact(DisplayName = "Disponibilizar Curso Inv�lido Lan�a Exce��o")]
        [Trait("Categoria", "Gest�o de Conte�do - Curso")]
        public void Disponibilizar_CursoInvalido_LancaExcecao()
        {
            // Arrange
            var curso = new Curso(nome: "A",
                                  conteudoProgramatico: new ConteudoProgramatico("A"));

            // Act & Assert
            var exception = Assert.Throws<DisponibilizacaoCursoInvalidoException>(() => curso.DisponibilizarMatricula());
            Assert.False(curso.DisponivelMatricula);
        }

        [Fact(DisplayName = "Adicionar Aula V�lida Associa Curso")]
        [Trait("Categoria", "Gest�o de Conte�do - Curso")]
        public void AdicionarAula_Valida_AssociaCurso()
        {
            // Arrange
            var curso = new Curso(nome: "Curso de C#",
                                  conteudoProgramatico: new ConteudoProgramatico("Conte�do Program�tico do Curso de C#"));
            var quantidadeAulasAntes = curso.Aulas?.Count ?? 0;
            var tituloDaAula = "Introdu��o";

            // Act
            curso.AdicionarAula(tituloDaAula, 
                                conteudo: "Apresenta��o do curso, do professor e dos objetivos do curso.");

            // Assert
            var quantidadeAulasDepois = curso.Aulas?.Count ?? 0;
            Assert.Equal(quantidadeAulasAntes + 1, quantidadeAulasDepois);
            Assert.Contains(curso.Aulas ?? [], a => a.Titulo.Equals(tituloDaAula, StringComparison.OrdinalIgnoreCase));
        }


        [Fact(DisplayName = "Adicionar Aula Inv�lida Lan�a Exce��o")]
        [Trait("Categoria", "Gest�o de Conte�do - Curso")]
        public void AdicionarAula_Invalida_LancaExcecao()
        {
            // Arrange
            var curso = new Curso(nome: "Curso de C#",
                                  conteudoProgramatico: new ConteudoProgramatico("Conte�do Program�tico do Curso de C#"));
            var quantidadeAulasAntes = curso.Aulas?.Count ?? 0;
            var tituloAula = "I";

            // Act && Assert
            var excecao = Assert.Throws<DomainException>(() => curso.AdicionarAula(tituloAula, conteudo: "A"));

            var regrasVioladas = excecao.RegrasVioladas;
            Assert.NotNull(regrasVioladas);
            Assert.Equal(2, regrasVioladas.Length);

            var quantidadeAulasDepois = curso.Aulas?.Count ?? 0;
            Assert.Equal(quantidadeAulasAntes, quantidadeAulasDepois);

            Assert.DoesNotContain(curso.Aulas ?? [], a => a.Titulo.Equals(tituloAula, StringComparison.OrdinalIgnoreCase));
        }
    }
}