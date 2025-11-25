using EducacaoOnline.Core.Messages;
using EducacaoOnline.Core.Messages.DomainNotifications;
using EducacaoOnline.PagamentoEFaturamento.Application.Events;
using EducacaoOnline.PagamentoEFaturamento.Domain;
using MediatR;

namespace EducacaoOnline.PagamentoEFaturamento.Application.Commands
{
    public class PagamentoCommandHandler : IRequestHandler<RealizarPagamentoCommand, bool>
    {
        private readonly IPagamentoRepository _pagamentoRepository;
        private readonly IMediator _mediator;

        public PagamentoCommandHandler(IPagamentoRepository pagamentoRepository,
                                       IMediator mediator)
        {
            _pagamentoRepository = pagamentoRepository;
            _mediator = mediator;
        }

        public async Task<bool> Handle(RealizarPagamentoCommand message, CancellationToken cancellationToken)
        {
            if (!ValidarComando(message)) return false;

            var pagamento = new Pagamento(idMatricula: message.IdMatricula);

            if (ProcessarPagamento(message))
            {
                pagamento.AtualizarStatus(StatusPagamento.Confirmado);
                var matricula = message.Matricula;
                matricula.Ativar();
                pagamento.AdicionarEvento(new PagamentoRealizadoEvent(idPagamento: pagamento.Id,
                                                                      idMatricula: message.IdMatricula));
            } else
            {
                pagamento.AtualizarStatus(StatusPagamento.Recusado);
                pagamento.AdicionarEvento(new PagamentoRecusadoEvent(idPagamento: pagamento.Id,
                                                                     idMatricula: message.IdMatricula,
                                                                     errosCartao: message.ErrosCartao));
            }

            _pagamentoRepository.Adicionar(pagamento);
            await _pagamentoRepository.UnitOfWork.Commit();

            return pagamento.Status.Equals(StatusPagamento.Confirmado);
        }

        bool ValidarComando(Command message)
        {
            if (message.EhValido()) return true;

            foreach (var error in message.Erros)
            {
                _mediator.Publish(new NotificacaoDominio(message.MessageType, error.ErrorMessage));
            }

            return false;
        }

        bool ProcessarPagamento(RealizarPagamentoCommand message)
        {
            // TODO: Código provisório. Implementar chamada de gateway externo.
            if (message.DadosCartao.NomeTitular is "Teste Falha")
            {
                message.ErrosCartao.Add(new FluentValidation.Results.ValidationFailure("DadosCartao.NomeTitular", "Pagamento recusado pelo emissor do cartão."));
                return false;
            }
            return true;
        }
    }
}