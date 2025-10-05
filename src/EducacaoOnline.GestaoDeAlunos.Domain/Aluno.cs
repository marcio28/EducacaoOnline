
using EducacaoOnline.GestaoDeConteudo.Domain;
using System.Collections.ObjectModel;

namespace EducacaoOnline.GestaoDeAlunos.Domain
{
    public class Aluno : Entity
    {
        public Collection<Matricula> Matriculas { get; private set; } = [];

        public Matricula IniciarMatricula(Guid idCurso)
        {
            var matricula = new Matricula(idAluno: this.Id,
                                          idCurso: idCurso);

            Matriculas.Add(matricula);

            return matricula;
        }
    }
}
