
using EducacaoOnline.Core.DomainObjects;

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
            var cursoSelecionado = new Curso(disponivelMatricula: true);
            var quatidadeMatriculasAntes = aluno.Matriculas.Count;

            // Act
            var matricula = aluno.IniciarMatricula(cursoSelecionado);

            // Assert
            Assert.Equal(0, aluno.ValidationResult?.Errors.Count ?? 0);
            var quatidadeMatriculasDepois = aluno.Matriculas.Count;
            Assert.Equal(quatidadeMatriculasAntes + 1, quatidadeMatriculasDepois);
            Assert.Equal(aluno.Id, matricula.IdAluno);
            Assert.Equal(cursoSelecionado.Id, matricula.IdCurso);
            Assert.Equal(StatusMatricula.AguardandoPagamento, matricula.Status);
        }

        [Fact(DisplayName = "Iniciar Matrícula Indisponível Lança Exceção")]
        [Trait("Categoria", "Gestão de Alunos - Aluno")]
        public void IniciarMatricula_Indisponivel_LancaExcecao()
        {
            // Arrange
            var aluno = new Aluno();
            var cursoSelecionado = new Curso(disponivelMatricula: false);

            // Act && Assert
            var excecao = Assert.Throws<DomainException>(() => aluno.IniciarMatricula(cursoSelecionado));
        }
    }
}
