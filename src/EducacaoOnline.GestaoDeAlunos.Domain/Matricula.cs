
using EducacaoOnline.Core.DomainObjects;

namespace EducacaoOnline.GestaoDeAlunos.Domain
{
    public class Matricula : Entity
    {
        public Guid IdAluno { get; }
        public Guid IdCurso { get; }
        public StatusMatricula Status { get; private set; } = StatusMatricula.AguardandoPagamento;
        public HistoricoAprendizado HistoricoAprendizado { get; private set; } = new();
        public Certificado? Certificado { get; private set; }

        public Matricula(Guid idAluno, Guid idCurso)
        {
            IdAluno = idAluno;
            IdCurso = idCurso;
        }
    }
}