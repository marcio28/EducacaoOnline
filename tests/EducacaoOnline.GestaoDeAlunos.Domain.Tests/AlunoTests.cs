
namespace EducacaoOnline.GestaoDeAlunos.Domain.Tests
{
    public class AlunoTests
    {
        [Fact(DisplayName = "Iniciar Matrícula Sem Violações Aguarda Pagamento")]
        [Trait("Categoria", "Gestão de Alunos - Aluno")]
        public void IniciarMatricula_SemViolacoes_AguardaPagamento()
        {
            // Arrange
            var aluno = new Aluno();
            var idCursoSelecionado = Guid.NewGuid();
            var quatidadeMatriculasAntes = aluno.Matriculas.Count;

            // Act
            var matricula = aluno.IniciarMatricula(idCurso: idCursoSelecionado);

            // Assert
            Assert.Equal(0, aluno.ValidationResult?.Errors.Count ?? 0);
            var quatidadeMatriculasDepois = aluno.Matriculas.Count;
            Assert.Equal(quatidadeMatriculasAntes + 1, quatidadeMatriculasDepois);
            Assert.Equal(aluno.Id, matricula.IdAluno);
            Assert.Equal(idCursoSelecionado, matricula.IdCurso);
            Assert.Equal(StatusMatricula.AguardandoPagamento, matricula.Status);
        }
    }
}
