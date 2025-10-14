using MediatR;

namespace EducacaoOnline.Core.Messages.Notifications
{
    public class DomainNotification : Message, INotification
    {
        public Guid Id { get; }
        public DateTime DataHora { get; }
        public string Chave { get; }
        public string Valor { get; }
        public int Versao { get; }

        public DomainNotification(string chave, string valor)
        {
            Id = Guid.NewGuid();
            DataHora = DateTime.Now;
            Chave = chave;
            Valor = valor;
            Versao = 1;
        }
    }
}