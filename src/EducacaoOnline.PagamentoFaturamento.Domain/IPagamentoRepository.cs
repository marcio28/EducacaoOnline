using EducacaoOnline.Core.Data;

namespace EducacaoOnline.PagamentoEFaturamento.Domain
{
    public interface IPagamentoRepository : IRepository<Pagamento>
    {
        void Adicionar(Pagamento pagamento);
    }
}