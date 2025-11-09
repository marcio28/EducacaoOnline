
using EducacaoOnline.Core.Messages.ApplicationNotifications;
using EducacaoOnline.GestaoConteudo.Domain.Repositories;

namespace EducacaoOnline.GestaoConteudo.Domain.Services
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
            if (curso.EhValido() is false)
            {
                foreach (var erro in curso.Erros)
                {
                    _notifiable.AddNotification(new ApplicationNotification(erro.ErrorMessage));
                }
                return;
            }

            var cursoExistente = await _cursoRepository.ObterPorId(curso.Id, tokenDeCancelamento);
            if (cursoExistente is not null)
            {
                _notifiable.AddNotification(new ApplicationNotification("Curso já cadastrado."));
                return;
            }

            bool existeCursoComMesmoNome = await _cursoRepository.Existe(curso.Nome!, tokenDeCancelamento);
            if (existeCursoComMesmoNome)
            {
                _notifiable.AddNotification(new ApplicationNotification("Já existe um curso cadastrado com este nome."));
                return;
            }

            await _cursoRepository.Incluir(curso, tokenDeCancelamento);
            await _cursoRepository.UnitOfWork.Commit();
        }

        public Task Alterar(Curso curso, CancellationToken tokenDeCancelamento)
        {
            throw new NotImplementedException();
        }

        public async Task Excluir(Guid id, CancellationToken tokenDeCancelamento)
        {
            var curso = await _cursoRepository.ObterPorId(id, tokenDeCancelamento);
            if (curso is null)
            {
                _notifiable.AddNotification(new ApplicationNotification("Curso não encontrado."));
                return;
            }

            await _cursoRepository.Excluir(curso, tokenDeCancelamento);
            await _cursoRepository.UnitOfWork.Commit();
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