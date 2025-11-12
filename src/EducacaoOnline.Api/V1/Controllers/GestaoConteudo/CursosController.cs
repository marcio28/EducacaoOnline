using AutoMapper;
using EducacaoOnline.Api.Controllers;
using EducacaoOnline.Api.Extensions;
using EducacaoOnline.Core.Messages.ApplicationNotifications;
using EducacaoOnline.GestaoConteudo.Application.Models;
using EducacaoOnline.GestaoConteudo.Domain;
using EducacaoOnline.GestaoConteudo.Domain.Services;
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
                                INotifiable notifiable) : base(httpContextAccessor, notifiable)
        {
            _cursoService = cursoService;
            _mapper = mapper;
        }

        //[ClaimsAuthorize("Cursos", "INCLUIR")] // Desabilitado temporariamente para executar os testes de integração
        [HttpPost]
        public async Task<ActionResult> IncluirCurso([FromBody] CursoModel cursoModel, CancellationToken tokenDeCancelamento)
        {
            if (ModelState.IsValid is false)
            {
                return RespostaCustomizada(ModelState);
            }

            await _cursoService.Incluir(_mapper.Map<Curso>(cursoModel), tokenDeCancelamento);

            return RespostaCustomizada(HttpStatusCode.Created);
        }

        [ClaimsAuthorize("Cursos", "VISUALIZAR")]
        [HttpGet]
        public async Task<ActionResult> ListarCursos(CancellationToken tokenDeCancelamento)
        {
            return RespostaCustomizada(HttpStatusCode.OK, _mapper.Map<IEnumerable<CursoModel>>(await _cursoService.Listar(tokenDeCancelamento)));
        }

        [ClaimsAuthorize("Cursos", "ALTERAR")]
        [HttpPut("{id:Guid}")]
        public async Task<IActionResult> AlterarCurso(Guid id, [FromBody] CursoModel cursoModel, CancellationToken tokenDeCancelamento)
        {
            if (id != cursoModel.Id)
            {
                AdicionarErroProcessamento("O Id informado não é o mesmo que foi passado no corpo da requisição.");
                return RespostaCustomizada();
            }

            if (ModelState.IsValid is false)
            {
                return RespostaCustomizada(ModelState);
            }

            await _cursoService.Alterar(_mapper.Map<Curso>(cursoModel), tokenDeCancelamento);

            return RespostaCustomizada(HttpStatusCode.NoContent);
        }

        [ClaimsAuthorize("Cursos", "VISUALIZAR")]
        [HttpGet("{id:Guid}")]
        public async Task<ActionResult> ObterCursoPorId(Guid id, CancellationToken tokenDeCancelamento)
        {
            var curso = _mapper.Map<CursoModel>(await _cursoService.ObterPorId(id, tokenDeCancelamento));

            return RespostaCustomizada(HttpStatusCode.OK, curso);
        }

        [ClaimsAuthorize("Cursos", "EXCLUIR")]
        [HttpDelete("{id:Guid}")]
        public async Task<ActionResult> ExcluirCurso(Guid id, CancellationToken tokenDeCancelamento)
        {
            if (id == Guid.Empty)
            {
                AdicionarErroProcessamento("O id não pode ser vazio.");
                return RespostaCustomizada();
            }

            await _cursoService.Excluir(id, tokenDeCancelamento);

            return RespostaCustomizada(HttpStatusCode.NoContent);
        }
    }
}