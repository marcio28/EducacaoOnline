using EducacaoOnline.Core.Data;
using EducacaoOnline.GestaoAlunos.Data.Context;
using EducacaoOnline.GestaoAlunos.Domain;
using EducacaoOnline.GestaoAlunos.Domain.Repositories;

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

        public async Task Incluir(Aluno aluno, CancellationToken tokenDeCancelamento)
        {
            await _context.Alunos.AddAsync(aluno, tokenDeCancelamento);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}