using EducacaoOnline.Core.Communication.Mediator;
using EducacaoOnline.Core.Messages.CommonMessages.IntegrationEvents;
using MediatR;

namespace EducacaoOnline.GestaoAlunos.Application.Events
{
    public class PagamentoEventHandler :
        INotificationHandler<PagamentoRealizadoEvent>,
        INotificationHandler<PagamentoRecusadoEvent>
    {
        private readonly IMediatorHandler _mediatorHandler;

        public PagamentoEventHandler(IMediatorHandler mediatorHandler)
        {
            _mediatorHandler = mediatorHandler;
        }

        public async Task Handle(PagamentoRealizadoEvent message, CancellationToken cancellationToken)
        {

            // TODO: Ativar matrícula
            return;
        }

        public async Task Handle(PagamentoRecusadoEvent message, CancellationToken cancellationToken)
        {
            // TODO: Recusar matrícula
            return;
        }
    }
}