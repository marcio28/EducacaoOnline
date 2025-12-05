using MediatR;

namespace EducacaoOnline.Core.Messages.CommonMessages.DomainNotifications
{
    public class DomainNotificationHandler : INotificationHandler<DomainNotification>
    {
        List<DomainNotification> _notificacoes;

        public DomainNotificationHandler()
        {
            _notificacoes = [];
        }

        public Task Handle(DomainNotification notificacao, CancellationToken tokenCancelamento)
        {
            _notificacoes.Add(notificacao);
            return Task.CompletedTask;
        }

        public virtual List<DomainNotification> ObterNotificacoes()
        {
            return _notificacoes;
        }

        public virtual bool TemNotificacoes()
        {
            return ObterNotificacoes().Count != 0;
        }

        public void Dispose()
        {
            _notificacoes = [];
        }
    }
}