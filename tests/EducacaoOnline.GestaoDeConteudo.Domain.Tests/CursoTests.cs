using EducacaoOnline.GestaoDeConteudo.Domain.Exceptions;
using EducacaoOnline.GestaoDeConteudo.Domain.Validators;

namespace EducacaoOnline.GestaoDeConteudo.Domain.Tests
{
    public class CursoTests
    {
        private Curso? _curso;

        [Fact(DisplayName = "Curso Novo Sem Erros É Válido")]
        [Trait("Categoria", "Gestão de Conteúdo - Curso")]
        public void CursoNovo_SemErros_DeveSerValido()
        {
            // Arrange & Act
            _curso = new(
                nome: "Curso de C#",
                conteudoProgramatico: new ConteudoProgramatico("Conteúdo Programático do Curso de C#"));

            // Assert
            Assert.True(_curso.EhValido());
            Assert.Equal(0, _curso.QuantidadeErros);
        }

        [Fact(DisplayName = "Curso Novo Com Erros É Inválido")]
        [Trait("Categoria", "Gestão de Conteúdo - Curso")]
        public void CursoNovo_ComErros_DeveSerInvalido()
        {
            // Arrange && Act
            _curso = new(
                nome: "A",
                conteudoProgramatico: new ConteudoProgramatico("A"));

            // Assert
            Assert.False(_curso.EhValido());
            Assert.Equal(2, _curso.QuantidadeErros);
            Assert.Contains(CursoValidator.TamanhoNomeErroMsg, _curso.Erros.Select(c => c.ErrorMessage));
            Assert.Contains(CursoValidator.TamanhoConteudoErroMsg, _curso.Erros.Select(c => c.ErrorMessage));
        }

        [Fact(DisplayName = "Disponibilizar Curso Válido Disponível Para Matrícula")]
        [Trait("Categoria", "Gestão de Conteúdo - Curso")]
        public void Disponibilizar_CursoValido_DeveFicarDisponivelMatricula()
        {
            // Arrange
            _curso = new(
                nome: "Curso de C#",
                conteudoProgramatico: new ConteudoProgramatico("Conteúdo Programático do Curso de C#"));

            // Act
            _curso.DisponibilizarMatricula();

            // Assert
            Assert.True(_curso.EhValido());
            Assert.True(_curso.DisponivelMatricula);
        }

        [Fact(DisplayName = "Disponibilizar Curso Inválido Lança Exceção")]
        [Trait("Categoria", "Gestão de Conteúdo - Curso")]
        public void Disponibilizar_CursoInvalido_DeveLancarExcecao()
        {
            // Arrange
            _curso = new(
                nome: "A",
                conteudoProgramatico: new ConteudoProgramatico("A"));

            // Act & Assert
            Assert.False(_curso.EhValido());
            var exception = Assert.Throws<DisponibilizacaoCursoInvalidoException>(() => _curso.DisponibilizarMatricula());
            Assert.False(_curso.DisponivelMatricula);
        }

        [Fact(DisplayName = "Adicionar Aula Válida Associada A Curso")]
        [Trait("Categoria", "Gestão de Conteúdo - Curso")]
        public void AdicionarAula_Valida_DeveSerAssociadaACurso()
        {
            // Arrange
            _curso = new(
                nome: "Curso de C#",
                conteudoProgramatico: new ConteudoProgramatico("Conteúdo Programático do Curso de C#"));

            var quantidadeAulasAntes = _curso.QuantidadeAulas;
            var tituloDaAula = "Introdução";

            // Act
            var aula = _curso.AdicionarAula(
                tituloDaAula,
                conteudo: "Apresentação do curso, do professor e dos objetivos do curso.");

            // Assert
            Assert.True(aula.EhValido());
            Assert.Equal(quantidadeAulasAntes + 1, _curso.QuantidadeAulas);
            Assert.Contains(_curso.Aulas ?? [], a => a.Titulo.Equals(tituloDaAula, StringComparison.OrdinalIgnoreCase));
        }


        [Fact(DisplayName = "Adicionar Aula Inválida Não Adicionada e Lança Exceção")]
        [Trait("Categoria", "Gestão de Conteúdo - Curso")]
        public void AdicionarAula_Invalida_DeveNaoSerAdicionadaELancarExcecao()
        {
            // Arrange
            _curso = new(
                nome: "Curso de C#",
                conteudoProgramatico: new ConteudoProgramatico("Conteúdo Programático do Curso de C#"));

            var quantidadeAulasAntes = _curso.QuantidadeAulas;

            var tituloAula = "I";

            // Act & Assert
            var exception = Assert.Throws<AulaInvalidaException>(() => _curso.AdicionarAula(tituloAula, conteudo: "A"));
            Assert.Equal(quantidadeAulasAntes, _curso.QuantidadeAulas);
            Assert.DoesNotContain(_curso.Aulas ?? [], a => a.Titulo.Equals(tituloAula, StringComparison.OrdinalIgnoreCase));
        }
    }
}