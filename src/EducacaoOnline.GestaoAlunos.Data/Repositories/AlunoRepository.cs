using EducacaoOnline.Core.Data;
using EducacaoOnline.GestaoAlunos.Domain;
using EducacaoOnline.GestaoAlunos.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace EducacaoOnline.GestaoAlunos.Data.Repositories
{
    public class AlunoRepository : IAlunoRepository
    {
        private readonly GestaoAlunosContext _context;

        public AlunoRepository(GestaoAlunosContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        public async Task Incluir(Aluno aluno, CancellationToken tokenCancelamento)
        {
            await _context.Alunos.AddAsync(aluno, tokenCancelamento);
        }

        public async Task<Aluno?> ObterPorId(Guid id, CancellationToken tokenCancelamento)
        {
            return await _context.Alunos.FirstOrDefaultAsync(a => a.Id == id, tokenCancelamento);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}