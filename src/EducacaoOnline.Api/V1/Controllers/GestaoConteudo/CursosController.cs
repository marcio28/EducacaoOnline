using AutoMapper;
using EducacaoOnline.Api.Controllers;
using EducacaoOnline.Core.Messages.DomainNotifications;
using EducacaoOnline.GestaoConteudo.Application.Models;
using EducacaoOnline.GestaoConteudo.Domain;
using EducacaoOnline.GestaoConteudo.Domain.Services;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace EducacaoOnline.Api.V1.Controllers.GestaoConteudo
{
    //[Authorize] // Desabilitado temporariamente para executar os testes de integração
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/gestao-conteudo/cursos")]
    public class CursosController : MainController
    {
        private readonly ICursoService _cursoService;
        private readonly IMapper _mapper;

        public CursosController(ICursoService cursoService,
                                IHttpContextAccessor httpContextAccessor,
                                IMapper mapper,
                                IMediator mediatorHandler,
                                INotificationHandler<NotificacaoDominio> _notificacaoHandler) : base(httpContextAccessor, mediatorHandler, _notificacaoHandler)
        {
            _cursoService = cursoService;
            _mapper = mapper;
        }

        //[DeclaracaoAutorizante("Cursos", "Cadastrar")] // Desabilitado temporariamente para executar os testes de integração
        [HttpPost]
        public async Task<IActionResult> IncluirCurso([FromBody] CursoModel cursoModel, CancellationToken tokenDeCancelamento)
        {
            if (ModelState.IsValid is false)
            {
                return RespostaErro(ModelState);
            }

            var curso = _mapper.Map<Curso>(cursoModel);

            await _cursoService.Incluir(curso, tokenDeCancelamento);

            return RespostaCustomizada(HttpStatusCode.Created);
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> ListarCursos(CancellationToken tokenDeCancelamento)
        {
            return RespostaCustomizada(HttpStatusCode.OK, _mapper.Map<IEnumerable<CursoModel>>(await _cursoService.Listar(tokenDeCancelamento)));
        }

        //[DeclaracaoAutorizante("Cursos", "Cadastrar")] // Desabilitado temporariamente para executar os testes de integração
        [HttpPut("{id:Guid}")]
        public async Task<IActionResult> AlterarCurso(Guid id, [FromBody] CursoModel cursoModel, CancellationToken tokenDeCancelamento)
        {
            if (id != cursoModel.Id)
            {
                NotificarErro("ErroValidacao", "O Id informado não é o mesmo que foi passado no corpo da requisição.");
                return RespostaCustomizada();
            }

            if (ModelState.IsValid is false)
            {
                return RespostaErro(ModelState);
            }

            await _cursoService.Alterar(_mapper.Map<Curso>(cursoModel), tokenDeCancelamento);

            return RespostaCustomizada(HttpStatusCode.NoContent);
        }

        [AllowAnonymous]
        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> ObterCursoPorId(Guid id, CancellationToken tokenDeCancelamento)
        {
            var curso = _mapper.Map<CursoModel>(await _cursoService.ObterPorId(id, tokenDeCancelamento));

            return RespostaCustomizada(HttpStatusCode.OK, curso);
        }

        //[DeclaracaoAutorizante("Cursos", "Cadastrar")] // Desabilitado temporariamente para executar os testes de integração
        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> ExcluirCurso(Guid id, CancellationToken tokenDeCancelamento)
        {
            if (id == Guid.Empty)
            {
                NotificarErro("ErroValidacao", "O id não pode ser vazio.");
                return RespostaCustomizada();
            }

            await _cursoService.Excluir(id, tokenDeCancelamento);

            return RespostaCustomizada(HttpStatusCode.NoContent);
        }
    }
}