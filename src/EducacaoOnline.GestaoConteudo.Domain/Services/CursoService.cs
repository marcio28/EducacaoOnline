
using EducacaoOnline.Core.Messages.ApplicationNotifications;
using EducacaoOnline.Core.Messages.DomainNotifications;
using EducacaoOnline.GestaoConteudo.Domain.Repositories;
using MediatR;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace EducacaoOnline.GestaoConteudo.Domain.Services
{
    public class CursoService : ICursoService
    {
        private readonly ICursoRepository _cursoRepository;
        private readonly IMediator _mediator;

        public CursoService(ICursoRepository cursoRepository,
                            IMediator mediator)
        {
            _cursoRepository = cursoRepository;
            _mediator = mediator;
        }   

        public async Task Incluir(Curso curso, CancellationToken tokenDeCancelamento)
        {
            if (curso.EhValido() is false)
            {
                foreach (var erro in curso.Erros)
                {
                    await _mediator.Publish(new NotificacaoDominio("Curso", erro.ErrorMessage), tokenDeCancelamento);
                }
                return;
            }

            var cursoExistente = await _cursoRepository.ObterPorId(curso.Id, tokenDeCancelamento);
            if (cursoExistente is not null)
            {
                await _mediator.Publish(new NotificacaoDominio("Curso", "Curso já cadastrado."), tokenDeCancelamento);
                return;
            }

            bool existeCursoComMesmoNome = await _cursoRepository.Existe(curso.Nome!, tokenDeCancelamento);
            if (existeCursoComMesmoNome)
            {
                await _mediator.Publish(new NotificacaoDominio("Curso", "Já existe um curso cadastrado com este nome."), tokenDeCancelamento);
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
                await _mediator.Publish(new NotificacaoDominio("Curso", "Curso não encontrado."), tokenDeCancelamento);
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