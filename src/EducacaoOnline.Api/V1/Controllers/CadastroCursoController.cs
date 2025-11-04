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
    [Authorize]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/cursos")]
    public class CadastroCursoController : MainController
    {
        private readonly ICursoService _cursoService;
        private readonly IMapper _mapper;
        public CadastroCursoController(ICursoService cursoService,
                                    INotifiable notifiable,
                                    IMapper mapper) : base(notifiable)
        {
            _cursoService = cursoService;
            _mapper = mapper;
        }

        [ClaimsAuthorize("Cursos", "INCLUIR")]
        [HttpPost]
        public async Task<ActionResult> Incluir([FromBody] CursoModel request, CancellationToken tokenDeCancelamento)
        {
            if (!ModelState.IsValid)
            {
                return RespostaCustomizada(ModelState);
            }

            await _cursoService.Incluir(_mapper.Map<Curso>(request), tokenDeCancelamento);

            return RespostaCustomizada(HttpStatusCode.Created);
        }

        [HttpGet]
        public async Task<ActionResult> Listar(CancellationToken tokenDeCancelamento)
        {
            return RespostaCustomizada(HttpStatusCode.OK, _mapper.Map<IEnumerable<CursoModel>>(await _cursoService.Listar(tokenDeCancelamento)));
        }

        [ClaimsAuthorize("Cursos", "ALTERAR")]
        [HttpPut("{id:Guid}")]
        public async Task<IActionResult> Alterar(Guid id, [FromBody] CursoModel request, CancellationToken tokenDeCancelamento)
        {
            if (id != request.Id)
            {
                AdicionarErroProcessamento("O id informado não é o mesmo que foi passado no body");
                return RespostaCustomizada();
            }
            if (!ModelState.IsValid)
            {
                return RespostaCustomizada(ModelState);
            }

            await _cursoService.Alterar(_mapper.Map<Curso>(request), tokenDeCancelamento);

            return RespostaCustomizada(HttpStatusCode.NoContent);
        }

        [HttpGet("{id:Guid}")]
        public async Task<ActionResult> ObterPorId(Guid id, CancellationToken tokenDeCancelamento)
        {
            var categoria = _mapper.Map<CursoModel>(await _cursoService.ObterPorId(id, tokenDeCancelamento));

            return RespostaCustomizada(HttpStatusCode.OK, categoria);
        }

        [ClaimsAuthorize("Cursos", "EXCLUIR")]
        [HttpDelete("{id:Guid}")]
        public async Task<ActionResult> Excluir(Guid id, CancellationToken tokenDeCancelamento)
        {
            await _cursoService.Excluir(id, tokenDeCancelamento);

            return RespostaCustomizada(HttpStatusCode.NoContent);
        }
    }
}