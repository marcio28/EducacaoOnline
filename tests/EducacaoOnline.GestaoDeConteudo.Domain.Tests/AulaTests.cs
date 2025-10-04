using System.Collections.ObjectModel;
using Xunit;

namespace EducacaoOnline.GestaoDeConteudo.Domain.Tests
{
    public class AulaTests
    {
        [Fact(DisplayName = "Aula Nova Válida")]
        [Trait("Categoria", "Gestão de Conteúdo - Aula")]
        public void CriarAula_AulaNova_DeveSerValida()
        {
            // Arrange & Act
            var aula = new Aula(idCurso: Guid.NewGuid(),
                                titulo: "Introdução", 
                                conteudo: "Apresentação do curso, do professor e dos objetivos do curso.");

            // Assert
            Assert.True(aula.EhValido());
            Assert.Equal(0, aula.ValidationResult?.Errors.Count);
        }

        [Fact(DisplayName = "Aula Nova Inválida")]
        [Trait("Categoria", "Gestão de Conteúdo - Aula")]
        public void CriarAula_AulaNova_DeveSerInvalida()
        {
            // Arrange & Act
            var aula = new Aula(idCurso: Guid.Empty,
                                titulo: "I",
                                conteudo: "A");

            // Assert
            Assert.False(aula.EhValido());
            Assert.Equal(3, aula.ValidationResult?.Errors.Count);
        }
    }
}
