using System;
using System.Reflection;
using Xunit;
using EducacaoOnline.GestaoAlunos.Domain;
using EducacaoOnline.Core.Messages;
using EducacaoOnline.Core.DomainObjects;

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

        [Fact(DisplayName = "Gerar certificado, matrícula ativa e curso concluído, gera certificado")]
        [Trait("Categoria", "Gestão de Alunos - Matricula")]
        public void GerarCertificado_MatriculaAtivaECursoConcluido_DeveGerarCertificado()
        {
            // Arrange
            var idAluno = Guid.NewGuid();
            var idCurso = Guid.NewGuid();
            var matricula = new Matricula(idAluno, idCurso);
            matricula.AtivarMatricula();

            // cria e define histórico como concluído via reflection
            var historico = new HistoricoAprendizado(matricula.Id);
            var tipoHistorico = typeof(HistoricoAprendizado);
            var propriedadeConcluido = tipoHistorico.GetProperty("Concluido", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            propriedadeConcluido!.SetValue(historico, true);

            // atribui HistoricoAprendizado à matrícula via reflection
            var tipoMatricula = typeof(Matricula);
            var propriedadeHistorico = tipoMatricula.GetProperty("HistoricoAprendizado", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            propriedadeHistorico!.SetValue(matricula, historico);

            var dataEmissao = DateTime.Now;

            // Act
            matricula.GerarCertificado(dataEmissao);

            // Assert
            Assert.NotNull(matricula.Certificado);
            Assert.Equal(matricula.Id, matricula.Certificado!.IdMatricula);
            Assert.Equal(idAluno, matricula.Certificado.IdAluno);
            Assert.Equal(idCurso, matricula.Certificado.IdCurso);
            Assert.Equal(dataEmissao, matricula.Certificado.DataDeEmissao);
        }

        [Fact(DisplayName = "Gerar certificado, matrícula inativa, lança exceção de domínio")]
        [Trait("Categoria", "Gestão de Alunos - Matricula")]
        public void GerarCertificado_MatriculaInativa_DeveLancarDominioException()
        {
            // Arrange
            var matricula = new Matricula(idAluno: Guid.NewGuid(), idCurso: Guid.NewGuid());

            // cria e define histórico como concluído via reflection
            var historico = new HistoricoAprendizado(matricula.Id);
            var tipoHistorico = typeof(HistoricoAprendizado);
            var propriedadeConcluido = tipoHistorico.GetProperty("Concluido", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            propriedadeConcluido!.SetValue(historico, true);

            var tipoMatricula = typeof(Matricula);
            var propriedadeHistorico = tipoMatricula.GetProperty("HistoricoAprendizado", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            propriedadeHistorico!.SetValue(matricula, historico);

            // Act & Assert
            var ex = Assert.Throws<DomainException>(() => matricula.GerarCertificado(DateTime.Now));
            Assert.Equal("Não é possível gerar o certificado, porque a matrícula não está ativa.", ex.Message);
        }

        [Fact(DisplayName = "Gerar certificado, histórico não concluído, lança exceção de domínio")]
        [Trait("Categoria", "Gestão de Alunos - Matricula")]
        public void GerarCertificado_HistoricoNaoConcluido_DeveLancarDomainException()
        {
            // Arrange
            var matricula = new Matricula(idAluno: Guid.NewGuid(), idCurso: Guid.NewGuid());
            matricula.AtivarMatricula();

            // historico não atribuído (null) -> deve falhar
            var ex1 = Assert.Throws<DomainException>(() => matricula.GerarCertificado(DateTime.Now));
            Assert.Equal("Não é possível gerar o certificado, porque o curso ainda não foi concluído.", ex1.Message);

            // ou historico presente mas Concluido == false
            var historico = new HistoricoAprendizado(matricula.Id);
            var matriculaTipo = typeof(Matricula);
            var propHistorico = matriculaTipo.GetProperty("HistoricoAprendizado", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            propHistorico!.SetValue(matricula, historico);

            var ex2 = Assert.Throws<DomainException>(() => matricula.GerarCertificado(DateTime.Now));
            Assert.Equal("Não é possível gerar o certificado, porque o curso ainda não foi concluído.", ex2.Message);
        }

        [Fact(DisplayName = "Gerar certificado, data no futuro, lança exceção de domínio")]
        [Trait("Categoria", "Gestão de Alunos - Matricula")]
        public void GerarCertificado_DataFutura_DeveLancarDomainException()
        {
            // Arrange
            var matricula = new Matricula(idAluno: Guid.NewGuid(), idCurso: Guid.NewGuid());
            matricula.AtivarMatricula();

            var historico = new HistoricoAprendizado(matricula.Id);
            var tipoHistorico = typeof(HistoricoAprendizado);
            var propriedadeConcluido = tipoHistorico.GetProperty("Concluido", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            propriedadeConcluido!.SetValue(historico, true);

            var tipoMatricula = typeof(Matricula);
            var propriedadeHistorico = tipoMatricula.GetProperty("HistoricoAprendizado", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            propriedadeHistorico!.SetValue(matricula, historico);

            var dataFutura = DateTime.Now.AddDays(1);

            // Act & Assert
            var ex = Assert.Throws<DomainException>(() => matricula.GerarCertificado(dataFutura));
            Assert.Equal("A data de emissão do certificado não pode ser no futuro.", ex.Message);
        }

        [Fact(DisplayName = "Gerar certificado, já gerado, não gera outro")]
        [Trait("Categoria", "Gestão de Alunos - Matricula")]
        public void GerarCertificado_JaGerado_DeveNaoGerarOutro()
        {
            // Arrange
            var idAluno = Guid.NewGuid();
            var idCurso = Guid.NewGuid();
            var matricula = new Matricula(idAluno, idCurso);
            matricula.AtivarMatricula();

            var historico = new HistoricoAprendizado(matricula.Id);
            var tipoHistorico = typeof(HistoricoAprendizado);
            var propriedadeConcluido = tipoHistorico.GetProperty("Concluido", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            propriedadeConcluido!.SetValue(historico, true);

            var tipoMatricula = typeof(Matricula);
            var propriedadeHistorico = tipoMatricula.GetProperty("HistoricoAprendizado", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            propriedadeHistorico!.SetValue(matricula, historico);

            var dataEmissao = DateTime.Now;
            matricula.GerarCertificado(dataEmissao);

            var certificadoOriginal = matricula.Certificado;

            // Act
            matricula.GerarCertificado(dataEmissao.AddDays(-1));

            // Assert
            Assert.Same(certificadoOriginal, matricula.Certificado);
            Assert.Equal(dataEmissao, matricula.Certificado!.DataDeEmissao);
        }
    }
}