using System;
using EducacaoOnline.GestaoAlunos.Domain;
using Xunit;

namespace EducacaoOnline.GestaoAlunos.Domain.Tests
{
    public class MatriculaTests
    {
        [Fact(DisplayName = "Matrícula nova, com argumentos válidos, define propriedades e aguarda pagamento")]
        [Trait("Categoria", "Gestão de Alunos - Matrícula")]
        public void MatriculaNova_ArgumentosValidos_DeveDefinirPropriedadesEAguardarPagamento()
        {
            // Arrange
            var idAluno = Guid.NewGuid();
            var idCurso = Guid.NewGuid();

            // Act
            var matricula = new Matricula(idAluno, idCurso);

            // Assert
            Assert.Equal(idAluno, matricula.IdAluno);
            Assert.Equal(idCurso, matricula.IdCurso);
            Assert.Equal(StatusMatricula.AguardandoPagamento, matricula.Status);
            Assert.Null(matricula.HistoricoAprendizado);
            Assert.Null(matricula.Certificado);
        }

        [Fact(DisplayName = "Matrícula nova, sem argumentos, mantém ids vazios e aguarda pagamento")]
        [Trait("Categoria", "Gestão de Alunos - Matrícula")]
        public void MatriculaNova_SemArgumentos_DeveManterIdsVaziosEAguardarPagamento()
        {
            // Act
            var matricula = new Matricula();

            // Assert
            Assert.Equal(Guid.Empty, matricula.IdAluno);
            Assert.Equal(Guid.Empty, matricula.IdCurso);
            Assert.Equal(StatusMatricula.AguardandoPagamento, matricula.Status);
        }

        [Fact(DisplayName = "Ativar matrícula altera status para Ativa")]
        [Trait("Categoria", "Gestão de Alunos - Matrícula")]
        public void AtivarMatricula_DeveAlterarStatusParaAtiva()
        {
            // Arrange
            var matricula = new Matricula(Guid.NewGuid(), Guid.NewGuid());

            // Act
            matricula.AtivarMatricula();

            // Assert
            Assert.Equal(StatusMatricula.Ativa, matricula.Status);
        }

        [Fact(DisplayName = "Cancelar matrícula altera status para Cancelada")]
        [Trait("Categoria", "Gestão de Alunos - Matrícula")]
        public void CancelarMatricula_DeveAlterarStatusParaCancelada()
        {
            // Arrange
            var matricula = new Matricula(Guid.NewGuid(), Guid.NewGuid());

            // Act
            matricula.CancelarMatricula();

            // Assert
            Assert.Equal(StatusMatricula.Cancelada, matricula.Status);
        }

        [Fact(DisplayName = "Expirar matrícula altera status para Expirada")]
        [Trait("Categoria", "Gestão de Alunos - Matrícula")]
        public void ExpirarMatricula_DeveAlterarStatusParaExpirada()
        {
            // Arrange
            var matricula = new Matricula(Guid.NewGuid(), Guid.NewGuid());

            // Act
            matricula.ExpirarMatricula();

            // Assert
            Assert.Equal(StatusMatricula.Expirada, matricula.Status);
        }
    }
}