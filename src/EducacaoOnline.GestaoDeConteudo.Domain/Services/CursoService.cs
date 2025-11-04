
using EducacaoOnline.Core.Messages.ApplicationNotifications;
using EducacaoOnline.GestaoDeConteudo.Domain.Repositories;

namespace EducacaoOnline.GestaoDeConteudo.Domain.Services
{
    public class CursoService : ICursoService
    {
        private readonly ICursoRepository _cursoRepository;
        private readonly INotifiable _notifiable;

        public CursoService(ICursoRepository cursoRepository,
                            INotifiable notifiable)
        {
            _cursoRepository = cursoRepository;
            _notifiable = notifiable;
        }   

        public async Task Incluir(Curso curso, CancellationToken tokenDeCancelamento)
        {
            var cursoExistente = await _cursoRepository.ObterPorId(curso.Id, tokenDeCancelamento);
            if (cursoExistente is not null)
            {
                _notifiable.AddNotification(new ApplicationNotification("Curso já cadastrado."));
                return;
            }

            bool existeCursoComMesmoNome = await _cursoRepository.Existe(curso.Nome, tokenDeCancelamento);
            if (existeCursoComMesmoNome)
            {
                _notifiable.AddNotification(new ApplicationNotification("Já existe um curso cadastrado com este nome."));
                return;
            }

            await _cursoRepository.Incluir(curso, tokenDeCancelamento);
        }

        public Task Alterar(Curso curso, CancellationToken tokenDeCancelamento)
        {
            throw new NotImplementedException();
        }

        public Task Excluir(Guid id, CancellationToken tokenDeCancelamento)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Curso>> Listar(CancellationToken tokenDeCancelamento)
        {
            throw new NotImplementedException();
        }
        public Task<Curso> ObterPorId(Guid id, CancellationToken tokenDeCancelamento)
        {
            throw new NotImplementedException();
        }
    }
}