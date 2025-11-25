using MediatR;

namespace EducacaoOnline.Core.Messages.DomainNotifications
{
    public class NotificacaoDominioHandler : INotificationHandler<NotificacaoDominio>
    {
        List<NotificacaoDominio> _notificacoes;

        public NotificacaoDominioHandler()
        {
            _notificacoes = [];
        }

        public Task Handle(NotificacaoDominio message, CancellationToken cancellationToken)
        {
            _notificacoes.Add(message);
            return Task.CompletedTask;
        }

        public virtual List<NotificacaoDominio> ObterNotificacoes()
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