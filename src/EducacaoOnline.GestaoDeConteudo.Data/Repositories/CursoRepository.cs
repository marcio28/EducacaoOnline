
using EducacaoOnline.GestaoDeConteudo.Data.Context;
using EducacaoOnline.GestaoDeConteudo.Domain;
using EducacaoOnline.GestaoDeConteudo.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace EducacaoOnline.GestaoDeConteudo.Data.Repositories
{
    public class CursoRepository : ICursoRepository
    {
        private readonly GestaoConteudoContext _context;

        public CursoRepository(GestaoConteudoContext context)
        {
            _context = context;
        }

        public async Task Incluir(Curso curso, CancellationToken tokenDeCancelamento)
        {
            await _context.Cursos.AddAsync(curso, tokenDeCancelamento);
        }

        public Task Alterar(Curso curso, CancellationToken tokenDeCancelamento)
        {
            return Task.FromResult(_context.Cursos.Update(curso));
        }

        public async Task<Curso?> ObterPorId(Guid id, CancellationToken tokenDeCancelamento)
        {
            return await _context.Cursos.FirstOrDefaultAsync(c => c.Id == id, tokenDeCancelamento);
        }

        public async Task<IList<Curso>> ListarSemContexto(CancellationToken tokenDeCancelamento)
        {
            return await _context
                            .Cursos
                            .AsNoTracking()
                            .ToListAsync(tokenDeCancelamento);
        }

        public async Task Excluir(Curso curso, CancellationToken tokenDeCancelamento)
        {
            await Task.FromResult(_context.Cursos.Remove(curso)).ConfigureAwait(false);
        }

        public async Task<bool> Existe(string nome, CancellationToken tokenDeCancelamento)
        {
            return await _context.Cursos.AnyAsync(c => c.Nome == nome, tokenDeCancelamento);
        }

        public async Task<int> SalvarMudancas(CancellationToken tokenDeCancelamento)
        {
            return await _context.SaveChangesAsync(tokenDeCancelamento);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}