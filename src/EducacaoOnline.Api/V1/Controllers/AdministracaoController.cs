using AutoMapper;
using EducacaoOnline.Api.Controllers;
using EducacaoOnline.Api.Extensions;
using EducacaoOnline.Api.Models;
using EducacaoOnline.Core.Messages.ApplicationNotifications;
using EducacaoOnline.GestaoDeConteudo.Domain;
using EducacaoOnline.GestaoDeConteudo.Domain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace EducacaoOnline.Api.V1.Controllers
{
    //[Authorize]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/admin")]
    public class AdministracaoController : MainController
    {
        private readonly ICursoService _cursoService;
        private readonly IMapper _mapper;

        public AdministracaoController(ICursoService cursoService, 
                                       INotifiable notifiable,
                                       IMapper mapper) : base(notifiable)
        {
            _cursoService = cursoService;
            _mapper = mapper;
        }

        //[ClaimsAuthorize("Cursos", "INCLUIR")]
        [HttpPost("cursos")]
        public async Task<ActionResult> IncluirCurso([FromBody] CursoModel cursoModel, CancellationToken tokenDeCancelamento)
        {
            if (!ModelState.IsValid)
            {
                return RespostaCustomizada(ModelState);
            }

            await _cursoService.Incluir(_mapper.Map<Curso>(cursoModel), tokenDeCancelamento);

            return RespostaCustomizada(HttpStatusCode.Created);
        }

        [HttpGet("cursos")]
        public async Task<ActionResult> ListarCursos(CancellationToken tokenDeCancelamento)
        {
            return RespostaCustomizada(HttpStatusCode.OK, _mapper.Map<IEnumerable<CursoModel>>(await _cursoService.Listar(tokenDeCancelamento)));
        }

        [ClaimsAuthorize("Cursos", "ALTERAR")]
        [HttpPut("cursos/{id:Guid}")]
        public async Task<IActionResult> AlterarCurso(Guid id, [FromBody] CursoModel cursoModel, CancellationToken tokenDeCancelamento)
        {
            if (id != cursoModel.Id)
            {
                AdicionarErroProcessamento("O Id informado não é o mesmo que foi passado no corpo da requisição.");
                return RespostaCustomizada();
            }

            if (!ModelState.IsValid)
            {
                return RespostaCustomizada(ModelState);
            }

            await _cursoService.Alterar(_mapper.Map<Curso>(cursoModel), tokenDeCancelamento);

            return RespostaCustomizada(HttpStatusCode.NoContent);
        }

        [HttpGet("cursos/{id:Guid}")]
        public async Task<ActionResult> ObterCursoPorId(Guid id, CancellationToken tokenDeCancelamento)
        {
            var curso = _mapper.Map<CursoModel>(await _cursoService.ObterPorId(id, tokenDeCancelamento));

            return RespostaCustomizada(HttpStatusCode.OK, curso);
        }

        [ClaimsAuthorize("Cursos", "EXCLUIR")]
        [HttpDelete("cursos/{id:Guid}")]
        public async Task<ActionResult> ExcluirCurso(Guid id, CancellationToken tokenDeCancelamento)
        {
            await _cursoService.Excluir(id, tokenDeCancelamento);

            return RespostaCustomizada(HttpStatusCode.NoContent);
        }
    }
}