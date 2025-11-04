namespace EducacaoOnline.GestaoDeConteudo.Domain.Services
{
    public interface ICursoService
    {
        Task Incluir(Curso curso, CancellationToken tokenDeCancelamento);
        Task Alterar(Curso curso, CancellationToken tokenDeCancelamento);
        Task Excluir(Guid id, CancellationToken tokenDeCancelamento);
        Task<IEnumerable<Curso>> Listar(CancellationToken tokenDeCancelamento);
        Task<Curso> ObterPorId(Guid id, CancellationToken tokenDeCancelamento);
    }
}