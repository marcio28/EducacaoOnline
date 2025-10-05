
using EducacaoOnline.Core.DomainObjects;
using EducacaoOnline.GestaoDeAlunos.Domain.Exceptions;
using System.Collections.ObjectModel;

namespace EducacaoOnline.GestaoDeAlunos.Domain
{
    public class Aluno : Entity
    {
        public Collection<Matricula> Matriculas { get; private set; } = [];

        public Matricula IniciarMatricula(Curso curso)
        {
            if (curso.DisponivelMatricula is false) throw new MatriculaCursoIndisponivelException();

            var matricula = new Matricula(idAluno: this.Id,
                                          idCurso: curso.Id);

            Matriculas.Add(matricula);

            return matricula;
        }
    }
}
