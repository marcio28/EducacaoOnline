using EducacaoOnline.Core.Messages.CommonMessages.DomainNotifications;
using EducacaoOnline.GestaoAlunos.Domain.Repositories;
using MediatR;

namespace EducacaoOnline.GestaoAlunos.Domain.Services
{
    public class AlunoService : IAlunoService
    {
        private readonly IAlunoRepository _alunoRepository;
        private readonly IMediator _mediator;

        public AlunoService(
            IAlunoRepository alunoRepository, IMediator mediator)
        {
            _alunoRepository = alunoRepository;
            _mediator = mediator;
        }   

        public async Task Incluir(Aluno aluno, CancellationToken tokenCancelamento)
        {
            if (aluno.EhValido() is false)
            {
                foreach (var erro in aluno.Erros)
                {
                    await _mediator.Publish(new DomainNotification("Aluno", erro.ErrorMessage), tokenCancelamento);
                }
                return;
            }

            var alunoExistente = await _alunoRepository.ObterPorId(aluno.Id, tokenCancelamento);
            if (alunoExistente is not null)
            {
                await _mediator.Publish(new DomainNotification("Aluno", "Aluno já cadastrado."), tokenCancelamento);
                return;
            }

            await _alunoRepository.Incluir(aluno, tokenCancelamento);
            await _alunoRepository.UnitOfWork.Commit();
        }

        public async Task AtivarMatricula(
            Guid idAluno, Guid idMatricula, CancellationToken tokenCancelamento)
        {
            var aluno = await _alunoRepository.ObterPorId(idAluno, tokenCancelamento);
            if (aluno is null)
            {
                await _mediator.Publish(new DomainNotification("Aluno", "Aluno não encontrado."), tokenCancelamento);
                return;
            }

            aluno.AtivarMatricula(idMatricula);
            return;
        }
    }
}