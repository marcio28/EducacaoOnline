using EducacaoOnline.Core.DomainObjects;
using EducacaoOnline.GestaoAlunos.Domain.Exceptions;
using System.Collections.ObjectModel;

namespace EducacaoOnline.GestaoAlunos.Domain
{
    public class Aluno : Entity, IAggregateRoot
    {
        public Collection<Matricula> Matriculas { get; private set; } = [];
        public int QuantidadeMatriculas => Matriculas.Count;

        public Aluno() { }

        public Aluno(Guid id) : base(id) { }

        public Matricula IniciarMatricula(Curso curso)
        {
            if (curso.DisponivelMatricula is false) throw new MatriculaCursoIndisponivelException();

            var matricula = new Matricula(idAluno: Id,
                                          idCurso: curso.Id);

            Matriculas.Add(matricula);

            return matricula;
        }
    }
}