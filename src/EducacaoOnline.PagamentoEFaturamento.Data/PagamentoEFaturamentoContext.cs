using EducacaoOnline.Core.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EducacaoOnline.PagamentoEFaturamento.Data
{
    public class PagamentoEFaturamentoContext : DbContext, IUnitOfWork
    {
        private readonly IMediator _mediator;

        public PagamentoEFaturamentoContext(DbContextOptions<PagamentoEFaturamentoContext> options,
                                            IMediator mediator) : base(options)
        {
            _mediator = mediator;
        }

        public async Task<bool> Commit()
        {
            var sucesso = await base.SaveChangesAsync() > 0;
            if (sucesso) await _mediator.PublicarEventos(this);

            return sucesso;
        }
    }
}