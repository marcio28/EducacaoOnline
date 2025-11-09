using EducacaoOnline.Core.DomainObjects;
using EducacaoOnline.PagamentoEFaturamento.Data;
using MediatR;

public static class MediatorExtension
{
    public static async Task PublicarEventos(this IMediator mediator, PagamentoEFaturamentoContext context)
    {
        var entityEntries = context.ChangeTracker
            .Entries<Entity>()
            .Where(e => e.Entity.Notificacoes is not null && e.Entity.Notificacoes.Count != 0);

        var comEventosEntityEntries = entityEntries
            .SelectMany(x => x.Entity.Notificacoes)
            .ToList();

        entityEntries
            .ToList()
            .ForEach(entityEntry => entityEntry.Entity.LimparEventos());

        var tasks = comEventosEntityEntries
            .Select(async (eventoDeDominio) => {
                await mediator.Publish(eventoDeDominio);
            });

        await Task.WhenAll(tasks);
    }
}