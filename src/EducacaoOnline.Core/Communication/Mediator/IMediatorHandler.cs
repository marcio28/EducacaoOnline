using EducacaoOnline.Core.Messages;
using EducacaoOnline.Core.Messages.CommonMessages.DomainEvents;
using EducacaoOnline.Core.Messages.CommonMessages.DomainNotifications;

namespace EducacaoOnline.Core.Communication.Mediator
{
    public interface IMediatorHandler
    {
        Task PublicarEvento<T>(T evento) where T : Event;
        Task PublicarNotificacao<T>(T notificacao) where T : DomainNotification;
        Task PublicarDomainEvent<T>(T notificacao) where T : DomainEvent;
    }
}