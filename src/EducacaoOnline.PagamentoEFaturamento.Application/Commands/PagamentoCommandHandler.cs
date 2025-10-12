using EducacaoOnline.PagamentoEFaturamento.Domain;
using MediatR;
using System.Runtime.CompilerServices;

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

        public Task<bool> Handle(RealizarPagamentoCommand message, CancellationToken cancellationToken)
        {
            if (!message.EhValido()) return Task.FromResult(false);

            var pagamento = new Pagamento(idMatricula: message.Matricula.Id);

            if (ProcessarPagamento(message))
            {
                pagamento.AtualizarStatus(StatusPagamento.Confirmado);
                var matricula = message.Matricula;
                matricula.Ativar();
            } else
            {
                pagamento.AtualizarStatus(StatusPagamento.Recusado);

            }

            _pagamentoRepository.Adicionar(pagamento);
            _pagamentoRepository.UnitOfWork.Commit();

            return Task.FromResult(pagamento.Status.Equals(StatusPagamento.Confirmado));
        }

        bool ProcessarPagamento(RealizarPagamentoCommand message)
        {
            // TODO: Processar pagamento com gateway externo
            return true;
        }
    }
}