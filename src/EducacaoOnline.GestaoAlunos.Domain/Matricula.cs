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

        public void Ativar()
        {
            Status = StatusMatricula.Ativa;
        }

        public void Cancelar()
        {
            Status = StatusMatricula.Cancelada;
        }

        public void Expirar()
        {
            Status = StatusMatricula.Expirada;
        }

        public void GerarCertificado(DateTime dataEmissao)
        {
            // Se já existir um certificado, não gera outro
            if (Certificado is not null)
                return;

            if (Status is not StatusMatricula.Ativa)
                throw new DomainException("Não é possível gerar o certificado, porque a matrícula não está ativa.");

            if (HistoricoAprendizado is null || HistoricoAprendizado.Concluido is false)
                throw new DomainException("Não é possível gerar o certificado, porque o curso ainda não foi concluído.");

            if (dataEmissao > DateTime.Now)
                throw new DomainException("A data de emissão do certificado não pode ser no futuro.");

            Certificado = new Certificado(idMatricula: Id, IdAluno, IdCurso, dataEmissao);
        }
    }
}