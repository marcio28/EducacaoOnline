using EducacaoOnline.Core.DomainObjects;

namespace EducacaoOnline.GestaoAlunos.Domain
{
    public class Matricula : Entity
    {
        public Guid IdAluno { get; }
        public Guid IdCurso { get; }
        public StatusMatricula Status { get; private set; } = StatusMatricula.AguardandoPagamento;
        public HistoricoAprendizado? HistoricoAprendizado { get; private set; }
        public Certificado? Certificado { get; private set; }

        public Matricula() { }

        public Matricula(Guid idAluno, Guid idCurso)
        {
            IdAluno = idAluno;
            IdCurso = idCurso;
        }

        public void AtivarMatricula()
        {
            Status = StatusMatricula.Ativa;
        }

        public void CancelarMatricula()
        {
            Status = StatusMatricula.Cancelada;
        }

        public void ExpirarMatricula()
        {
            Status = StatusMatricula.Expirada;
        }
    }
}