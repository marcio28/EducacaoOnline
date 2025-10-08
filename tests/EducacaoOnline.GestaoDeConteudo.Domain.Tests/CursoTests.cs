using EducacaoOnline.Core.DomainObjects;
using EducacaoOnline.GestaoDeConteudo.Domain.Exceptions;
using EducacaoOnline.GestaoDeConteudo.Domain.Validators;

namespace EducacaoOnline.GestaoDeConteudo.Domain.Tests
{
    public class CursoTests
    {
        [Fact(DisplayName = "Curso Novo Sem Erros É Válido")]
        [Trait("Categoria", "Gestão de Conteúdo - Curso")]
        public void CursoNovo_SemErros_EhValido()
        {
            // Arrange & Act
            var curso = new Curso(nome: "Curso de C#",
                                  conteudoProgramatico: new ConteudoProgramatico("Conteúdo Programático do Curso de C#"));

            // Assert
            Assert.True(curso.EhValido());
            Assert.Equal(0, curso.QuantidadeErros);
        }

        [Fact(DisplayName = "Curso Novo Com Erros É Inválido")]
        [Trait("Categoria", "Gestão de Conteúdo - Curso")]
        public void CursoNovo_ComErros_EhInvalido()
        {
            // Arrange && Act
            var curso = new Curso(nome: "A",
                                  conteudoProgramatico: new ConteudoProgramatico("A"));

            // Assert
            Assert.False(curso.EhValido());
            Assert.Equal(2, curso.QuantidadeErros);
            Assert.Contains(CursoValidator.TamanhoNomeErroMsg, curso.Erros.Select(c => c.ErrorMessage));
            Assert.Contains(CursoValidator.TamanhoConteudoErroMsg, curso.Erros.Select(c => c.ErrorMessage));
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
            Assert.True(curso.EhValido());
            Assert.True(curso.DisponivelMatricula);
        }

        [Fact(DisplayName = "Disponibilizar Curso Inválido Lança Exceção")]
        [Trait("Categoria", "Gestão de Conteúdo - Curso")]
        public void Disponibilizar_CursoInvalido_LancaExcecao()
        {
            // Arrange
            var curso = new Curso(nome: "A",
                                  conteudoProgramatico: new ConteudoProgramatico("A"));

            // Act & Assert
            Assert.False(curso.EhValido());
            var exception = Assert.Throws<DisponibilizacaoCursoInvalidoException>(() => curso.DisponibilizarMatricula());
            Assert.False(curso.DisponivelMatricula);
        }

        [Fact(DisplayName = "Adicionar Aula Válida Associa Curso")]
        [Trait("Categoria", "Gestão de Conteúdo - Curso")]
        public void AdicionarAula_Valida_AssociaCurso()
        {
            // Arrange
            var curso = new Curso(nome: "Curso de C#",
                                  conteudoProgramatico: new ConteudoProgramatico("Conteúdo Programático do Curso de C#"));
            var quantidadeAulasAntes = curso.QuantidadeAulas;
            var tituloDaAula = "Introdução";

            // Act
            var aula = curso.AdicionarAula(tituloDaAula, 
                                conteudo: "Apresentação do curso, do professor e dos objetivos do curso.");

            // Assert
            Assert.True(aula.EhValido());
            Assert.Equal(quantidadeAulasAntes + 1, curso.QuantidadeAulas);
            Assert.Contains(curso.Aulas ?? [], a => a.Titulo.Equals(tituloDaAula, StringComparison.OrdinalIgnoreCase));
        }


        [Fact(DisplayName = "Adicionar Aula Inválida Não Adiciona")]
        [Trait("Categoria", "Gestão de Conteúdo - Curso")]
        public void AdicionarAula_Invalida_NaoAdiciona()
        {
            // Arrange
            var curso = new Curso(nome: "Curso de C#",
                                  conteudoProgramatico: new ConteudoProgramatico("Conteúdo Programático do Curso de C#"));
            var quantidadeAulasAntes = curso.QuantidadeAulas;
            var tituloAula = "I";

            // Act
            var aula = curso.AdicionarAula(tituloAula, conteudo: "A");

            // Assert
            Assert.False(aula.EhValido());
            Assert.Equal(quantidadeAulasAntes, curso.QuantidadeAulas);
            Assert.DoesNotContain(curso.Aulas ?? [], a => a.Titulo.Equals(tituloAula, StringComparison.OrdinalIgnoreCase));
            Assert.Equal(2, aula.QuantidadeErros);
            Assert.Contains(AulaValidator.TamanhoTituloErroMsg, aula.Erros.Select(c => c.ErrorMessage));
            Assert.Contains(AulaValidator.TamanhoConteudoErroMsg, aula.Erros.Select(c => c.ErrorMessage));
        }
    }
}