using EducacaoOnline.Core.Data;

namespace EducacaoOnline.GestaoAlunos.Domain.Repositories
{
    public interface IAlunoRepository : IRepository<Aluno>
    {
        public Task Incluir(Aluno aluno, CancellationToken tokenCancelamento);
        public Task<Aluno?> ObterPorId(Guid id, CancellationToken tokenCancelamento);
    }
}