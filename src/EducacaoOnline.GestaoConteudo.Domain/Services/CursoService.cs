
using EducacaoOnline.Core.Messages.DomainNotifications;
using EducacaoOnline.GestaoConteudo.Domain.Repositories;
using MediatR;

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

        public async Task Alterar(Curso curso, CancellationToken tokenDeCancelamento)
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
            if (cursoExistente is null)
            {
                await _mediator.Publish(new NotificacaoDominio("Curso", "Curso não encontrado."), tokenDeCancelamento);
                return;
            }

            bool existeCursoComMesmoNome = await _cursoRepository.Existe(curso.Nome!, tokenDeCancelamento);
            // se já existe um curso com o mesmo nome e não é o próprio registro que está sendo alterado
            if (existeCursoComMesmoNome && !string.Equals(cursoExistente.Nome, curso.Nome, StringComparison.OrdinalIgnoreCase))
            {
                await _mediator.Publish(new NotificacaoDominio("Curso", "Já existe um curso cadastrado com este nome."), tokenDeCancelamento);
                return;
            }

            cursoExistente.Atualizar(curso.Nome!, curso.ConteudoProgramatico);

            await _cursoRepository.Alterar(cursoExistente, tokenDeCancelamento);
            await _cursoRepository.UnitOfWork.Commit();
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

        public async Task<IEnumerable<Curso>> Listar(CancellationToken tokenDeCancelamento)
        {
            var cursos = await _cursoRepository.ListarSemContexto(tokenDeCancelamento);
            return cursos;
        }

        public async Task<Curso> ObterPorId(Guid id, CancellationToken tokenDeCancelamento)
        {
            var curso = await _cursoRepository.ObterPorId(id, tokenDeCancelamento);
            if (curso is null)
            {
                await _mediator.Publish(new NotificacaoDominio("Curso", "Curso não encontrado."), tokenDeCancelamento);
                return default!;
            }

            return curso;
        }
    }
}