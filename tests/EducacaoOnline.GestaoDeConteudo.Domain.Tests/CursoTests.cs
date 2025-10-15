using EducacaoOnline.GestaoDeConteudo.Domain.Exceptions;
using EducacaoOnline.GestaoDeConteudo.Domain.Validators;

namespace EducacaoOnline.GestaoDeConteudo.Domain.Tests
{
    public class CursoTests
    {
        private Curso? _curso;

        [Fact(DisplayName = "Curso Novo Sem Erros � V�lido")]
        [Trait("Categoria", "Gest�o de Conte�do - Curso")]
        public void CursoNovo_SemErros_DeveSerValido()
        {
            // Arrange & Act
            _curso = new(
                nome: "Curso de C#",
                conteudoProgramatico: new ConteudoProgramatico("Conte�do Program�tico do Curso de C#"));

            // Assert
            Assert.True(_curso.EhValido());
            Assert.Equal(0, _curso.QuantidadeErros);
        }

        [Fact(DisplayName = "Curso Novo Com Erros � Inv�lido")]
        [Trait("Categoria", "Gest�o de Conte�do - Curso")]
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

        [Fact(DisplayName = "Disponibilizar Curso V�lido Dispon�vel Para Matr�cula")]
        [Trait("Categoria", "Gest�o de Conte�do - Curso")]
        public void Disponibilizar_CursoValido_DeveFicarDisponivelMatricula()
        {
            // Arrange
            _curso = new(
                nome: "Curso de C#",
                conteudoProgramatico: new ConteudoProgramatico("Conte�do Program�tico do Curso de C#"));

            // Act
            _curso.DisponibilizarMatricula();

            // Assert
            Assert.True(_curso.EhValido());
            Assert.True(_curso.DisponivelMatricula);
        }

        [Fact(DisplayName = "Disponibilizar Curso Inv�lido Lan�a Exce��o")]
        [Trait("Categoria", "Gest�o de Conte�do - Curso")]
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

        [Fact(DisplayName = "Adicionar Aula V�lida Associada A Curso")]
        [Trait("Categoria", "Gest�o de Conte�do - Curso")]
        public void AdicionarAula_Valida_DeveSerAssociadaACurso()
        {
            // Arrange
            _curso = new(
                nome: "Curso de C#",
                conteudoProgramatico: new ConteudoProgramatico("Conte�do Program�tico do Curso de C#"));

            var quantidadeAulasAntes = _curso.QuantidadeAulas;
            var tituloDaAula = "Introdu��o";

            // Act
            var aula = _curso.AdicionarAula(
                tituloDaAula,
                conteudo: "Apresenta��o do curso, do professor e dos objetivos do curso.");

            // Assert
            Assert.True(aula.EhValido());
            Assert.Equal(quantidadeAulasAntes + 1, _curso.QuantidadeAulas);
            Assert.Contains(_curso.Aulas ?? [], a => a.Titulo.Equals(tituloDaAula, StringComparison.OrdinalIgnoreCase));
        }


        [Fact(DisplayName = "Adicionar Aula Inv�lida N�o Adicionada")]
        [Trait("Categoria", "Gest�o de Conte�do - Curso")]
        public void AdicionarAula_Invalida_NaoDeveSerAdicionada()
        {
            // Arrange
            _curso = new(
                nome: "Curso de C#",
                conteudoProgramatico: new ConteudoProgramatico("Conte�do Program�tico do Curso de C#"));

            var quantidadeAulasAntes = _curso.QuantidadeAulas;

            var tituloAula = "I";

            // Act
            var aula = _curso.AdicionarAula(tituloAula, conteudo: "A");

            // Assert
            Assert.False(aula.EhValido());
            Assert.Equal(quantidadeAulasAntes, _curso.QuantidadeAulas);
            Assert.DoesNotContain(_curso.Aulas ?? [], a => a.Titulo.Equals(tituloAula, StringComparison.OrdinalIgnoreCase));
            Assert.Equal(2, aula.QuantidadeErros);
            Assert.Contains(AulaValidator.TamanhoTituloErroMsg, aula.Erros.Select(c => c.ErrorMessage));
            Assert.Contains(AulaValidator.TamanhoConteudoErroMsg, aula.Erros.Select(c => c.ErrorMessage));
        }
    }
}