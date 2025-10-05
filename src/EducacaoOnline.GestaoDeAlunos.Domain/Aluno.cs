
using EducacaoOnline.Core.DomainObjects;
using System.Collections.ObjectModel;

namespace EducacaoOnline.GestaoDeAlunos.Domain
{
    public class Aluno : Entity
    {
        public Collection<Matricula> Matriculas { get; private set; } = [];

        public Matricula IniciarMatricula(Curso curso)
        {
            if (curso.DisponivelMatricula is false) throw new DomainException(message: "Curso indisponível para matrícula.");

            var matricula = new Matricula(idAluno: this.Id,
                                          idCurso: curso.Id);

            Matriculas.Add(matricula);

            return matricula;
        }
    }
}
