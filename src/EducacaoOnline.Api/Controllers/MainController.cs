using EducacaoOnline.Core.Messages.DomainNotifications;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Net;
using System.Security.Claims;

namespace EducacaoOnline.Api.Controllers
{
    [ApiController]
    public class MainController : Controller
    {
        private readonly NotificacaoDominioHandler _notificacaoHandler;
        private readonly IMediator _mediatorHandler;

        protected Guid UsuarioId = Guid.Empty;

        protected MainController(IHttpContextAccessor httpContextAccessor,
                                 IMediator mediatorHandler,
                                 INotificationHandler<NotificacaoDominio> notificacaoHandler)
        {
            _notificacaoHandler = (NotificacaoDominioHandler)notificacaoHandler;
            _mediatorHandler = mediatorHandler;

            var usuario = httpContextAccessor.HttpContext?.User;

            if (usuario is null) return;

            if (usuario.Identity?.IsAuthenticated is false) return;

            var declaracao = usuario.FindFirst(ClaimTypes.NameIdentifier);

            if (declaracao is not null)
                UsuarioId = Guid.Parse(declaracao.Value);
        }

        protected IActionResult RespostaCustomizada(HttpStatusCode statusCode = HttpStatusCode.OK, object? result = null)
        {
            if (OperacaoValida())
            {
                return new ObjectResult(new
                {
                    success = true,
                    data = result
                })
                {
                    StatusCode = (int)statusCode
                };
            }

            return BadRequest(new
            {
                success = false,
                errors = _notificacaoHandler.ObterNotificacoes().Select(n => n.Valor)
            });
        }

        protected IActionResult RespostaErro(ModelStateDictionary modelState)
        {
            var erros = modelState.Values.SelectMany(e => e.Errors);
            List<string> mensagensErro =  [];

            foreach (var erro in erros)
            {
                mensagensErro.Add(erro.Exception == null ? erro.ErrorMessage : erro.Exception.Message);
            }

            return BadRequest(new
            {
                success = false,
                errors = mensagensErro
            });
        }

        protected bool OperacaoValida()
        {
            return _notificacaoHandler.TemNotificacoes() is false;
        }

        protected IEnumerable<string> ObterMensagensErro()
        {
            return [.. _notificacaoHandler.ObterNotificacoes().Select(n => n.Valor)];
        }

        protected void NotificarErro(string codigo, string mensagem)
        {
            _mediatorHandler.Publish(new NotificacaoDominio(codigo, mensagem));
        }
    }
}