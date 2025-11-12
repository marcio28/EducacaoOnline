using EducacaoOnline.GestaoAlunos.Domain.Validators;

namespace EducacaoOnline.GestaoAlunos.Domain.Tests
{
    public class HistoricoAprendizadoTests
    {
        [Fact(DisplayName = "Histórico de Aprendizado Sem Erros É Válido")]
        [Trait("Categoria", "Gestão de Alunos - Histórico de Aprendizado")]
        public void HistoricoAprendizado_SemErros_DeveSerValido()
        {
            // Arrange & Act
            var idMatricula = Guid.NewGuid();
            var historicoAprendizado = new HistoricoAprendizado(idMatricula);

            // Assert
            Assert.NotNull(historicoAprendizado);
            Assert.Equal(0, historicoAprendizado.QuantidadeErros);
            Assert.Equal(idMatricula, historicoAprendizado.IdMatricula);
            Assert.True(historicoAprendizado.EhValido());
        }

        [Fact(DisplayName = "Histórico de Aprendizado Com Erros É Inválido")]
        [Trait("Categoria", "Gestão de Alunos - Histórico de Aprendizado")]
        public void HistoricoAprendizado_ComErros_DeveSerInvalido()
        {
            // Arrange && Act
            var idMatricula = Guid.Empty;
            var historicoAprendizado = new HistoricoAprendizado(idMatricula);

            // Assert
            Assert.False(historicoAprendizado.EhValido());
            Assert.Equal(1, historicoAprendizado.QuantidadeErros);
            Assert.Contains(HistoricoAprendizadoValidator.IdMatriculaObrigatorioErroMsg, historicoAprendizado.Erros.Select(c => c.ErrorMessage));
        }
    }
}