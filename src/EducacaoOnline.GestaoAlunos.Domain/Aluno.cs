using EducacaoOnline.Core.DomainObjects;
using EducacaoOnline.GestaoAlunos.Domain.Exceptions;
using System.Collections.ObjectModel;

namespace EducacaoOnline.GestaoAlunos.Domain
{
    public class Aluno : Entity, IAggregateRoot
    {
        public string Nome { get; private set; } = string.Empty;
        public Collection<Matricula> Matriculas { get; private set; } = [];
        public int QuantidadeMatriculas => Matriculas.Count;

        protected Aluno() { }

        public Aluno(Guid id) : base(id) { }

        public Matricula IniciarMatricula(Guid idCurso)
        {
            if (idCurso == Guid.Empty) throw new MatriculaCursoInvalidoException();

            var matricula = new Matricula(
                idAluno: Id, idCurso: idCurso);

            Matriculas.Add(matricula);

            return matricula;
        }

        public void AtivarMatricula(Guid idMatricula)
        {
            var matricula = Matriculas.FirstOrDefault(m => m.Id == idMatricula);
            if (matricula is null) throw new MatriculaNaoEncontradaException();
            matricula.Ativar();
        }
    }
}