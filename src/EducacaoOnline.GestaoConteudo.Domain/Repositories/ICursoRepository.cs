using EducacaoOnline.Core.Data;

namespace EducacaoOnline.GestaoConteudo.Domain.Repositories
{
    public interface ICursoRepository : IRepository<Curso>
    {
        public Task Incluir(Curso curso, CancellationToken tokenDeCancelamento);
        public Task<Curso?> ObterPorId(Guid id, CancellationToken tokenDeCancelamento);
        public Task<IList<Curso>> ListarSemContexto(CancellationToken tokenDeCancelamento);
        public Task Alterar(Curso curso, CancellationToken tokenDeCancelamento);
        public Task Excluir(Curso curso, CancellationToken tokenDeCancelamento);
        public Task<bool> Existe(string nome, CancellationToken tokenDeCancelamento);
    }
}