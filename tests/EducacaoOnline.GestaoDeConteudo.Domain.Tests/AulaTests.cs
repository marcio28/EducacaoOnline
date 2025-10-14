using EducacaoOnline.GestaoDeConteudo.Domain.Validators;

namespace EducacaoOnline.GestaoDeConteudo.Domain.Tests
{
    public class AulaTests
    {
        [Fact(DisplayName = "Aula Nova Sem Erros É Válida")]
        [Trait("Categoria", "Gestão de Conteúdo - Aula")]
        public void AulaNova_SemErros_DeveSerValida()
        {
            // Arrange & Act
            var aula = new Aula(idCurso: Guid.NewGuid(),
                                titulo: "Introdução", 
                                conteudo: "Apresentação do curso, do professor e dos objetivos do curso.",
                                nomeArquivoMaterial: default);

            // Assert
            Assert.True(aula.EhValido());
            Assert.Equal(0, aula.QuantidadeErros);
        }

        [Fact(DisplayName = "Aula Nova Com Erros É Inválida")]
        [Trait("Categoria", "Gestão de Conteúdo - Aula")]
        public void AulaNova_ComErros_DeveSerInvalida()
        {
            // Arrange & Act
            var aula = new Aula(idCurso: Guid.Empty,
                                titulo: "I",
                                conteudo: "A",
                                nomeArquivoMaterial: default);

            // Assert
            Assert.False(aula.EhValido());
            Assert.Equal(3, aula.QuantidadeErros);
            Assert.Contains(AulaValidator.IdCursoObrigatorioErroMsg,
                            aula.Erros.Select(c => c.ErrorMessage));
            Assert.Contains(AulaValidator.TamanhoTituloErroMsg,
                            aula.Erros.Select(c => c.ErrorMessage));
            Assert.Contains(AulaValidator.TamanhoConteudoErroMsg,
                            aula.Erros.Select(c => c.ErrorMessage));
        }
    }
}