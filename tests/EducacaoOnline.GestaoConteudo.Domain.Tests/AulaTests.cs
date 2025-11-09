using EducacaoOnline.GestaoConteudo.Domain.Validators;

namespace EducacaoOnline.GestaoConteudo.Domain.Tests
{
    public class AulaTests
    {
        private Aula? _aula;

        [Fact(DisplayName = "Aula Nova Sem Erros É Válida")]
        [Trait("Categoria", "Gestão de Conteúdo - Aula")]
        public void AulaNova_SemErros_DeveSerValida()
        {
            // Arrange & Act
            _aula = new(
                idCurso: Guid.NewGuid(),
                titulo: "Introdução",
                conteudo: "Apresentação do curso, do professor e dos objetivos do curso.",
                nomeArquivoMaterial: default);

            // Assert
            Assert.True(_aula.EhValido());
            Assert.Equal(0, _aula.QuantidadeErros);
        }

        [Fact(DisplayName = "Aula Nova Com Erros É Inválida")]
        [Trait("Categoria", "Gestão de Conteúdo - Aula")]
        public void AulaNova_ComErros_DeveSerInvalida()
        {
            // Arrange & Act
            _aula = new(
                idCurso: Guid.Empty,
                titulo: "I",
                conteudo: "A",
                nomeArquivoMaterial: default);

            // Assert
            Assert.False(_aula.EhValido());
            Assert.Equal(3, _aula.QuantidadeErros);
            Assert.Contains(AulaValidator.IdCursoObrigatorioErroMsg, _aula.Erros.Select(c => c.ErrorMessage));
            Assert.Contains(AulaValidator.TamanhoTituloErroMsg, _aula.Erros.Select(c => c.ErrorMessage));
            Assert.Contains(AulaValidator.TamanhoConteudoErroMsg, _aula.Erros.Select(c => c.ErrorMessage));
        }
    }
}