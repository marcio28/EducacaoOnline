
using EducacaoOnline.GestaoDeConteudo.Domain.Validators;

namespace EducacaoOnline.GestaoDeConteudo.Domain.Tests
{
    public class AulaTests
    {
        [Fact(DisplayName = "Aula Nova Sem Violacoes É Válida")]
        [Trait("Categoria", "Gestão de Conteúdo - Aula")]
        public void AulaNova_SemViolacoes_EhValida()
        {
            // Arrange & Act
            var aula = new Aula(idCurso: Guid.NewGuid(),
                                titulo: "Introdução", 
                                conteudo: "Apresentação do curso, do professor e dos objetivos do curso.",
                                nomeArquivoMaterial: default);

            // Assert
            Assert.True(aula.EhValido());
            Assert.Equal(0, aula.ValidationResult?.Errors.Count);
        }

        [Fact(DisplayName = "Aula Nova Com Violações É Inválida")]
        [Trait("Categoria", "Gestão de Conteúdo - Aula")]
        public void AulaNova_ComViolacoes_EhInvalida()
        {
            // Arrange & Act
            var aula = new Aula(idCurso: Guid.Empty,
                                titulo: "I",
                                conteudo: "A",
                                nomeArquivoMaterial: default);

            // Assert
            Assert.False(aula.EhValido());
            Assert.Equal(3, aula.ValidationResult?.Errors.Count);
            Assert.Contains(AulaValidator.IdCursoObrigatorioErroMsg,
                            aula.ValidationResult?.Errors.Select(c => c.ErrorMessage) ?? []);
            Assert.Contains(AulaValidator.TamanhoTituloErroMsg,
                            aula.ValidationResult?.Errors.Select(c => c.ErrorMessage) ?? []);
            Assert.Contains(AulaValidator.TamanhoConteudoErroMsg,
                            aula.ValidationResult?.Errors.Select(c => c.ErrorMessage) ?? []);

        }
    }
}
