using EducacaoOnline.Core.Messages.ApplicationNotifications;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Net;
using System.Security.Claims;

namespace EducacaoOnline.Api.Controllers
{
    [ApiController]
    public class MainController : Controller
    {
        private readonly INotifiable _notifiable;
        protected Guid UsuarioId = Guid.Empty;

        protected MainController(IHttpContextAccessor httpContextAccessor,
                                 INotifiable notifiable)
        {
            _notifiable = notifiable;

            var usuario = httpContextAccessor.HttpContext?.User;

            if (usuario is null) return;

            if (usuario.Identity?.IsAuthenticated is false) return;

            var declaracao = usuario.FindFirst(ClaimTypes.NameIdentifier);

            if (declaracao is not null)
                UsuarioId = Guid.Parse(declaracao.Value);
        }

        protected ActionResult RespostaCustomizada(HttpStatusCode statusCode = HttpStatusCode.OK, object? result = null)
        {
            if (OperacaoValida())
            {
                return new ObjectResult(new
                {
                    Sucesso = true,
                    Data = result
                })
                {
                    StatusCode = (int)statusCode
                };
            }

            return BadRequest(new
            {
                Sucesso = false,
                Mensagens = _notifiable.GetNotifications().Select(n => n.Message).ToArray()
            });
        }
        protected ActionResult RespostaCustomizada(ModelStateDictionary modelState)
        {
            var erros = modelState.Values.SelectMany(e => e.Errors);
            foreach (var erro in erros)
            {
                var mensagemErro = erro.Exception == null ? erro.ErrorMessage : erro.Exception.Message;
                AdicionarErroProcessamento(mensagemErro);
            }

            return RespostaCustomizada();
        }

        protected ActionResult RespostaCustomizada(List<ApplicationNotification> notificacoes)
        {
            foreach (var notiticacao in notificacoes)
            {
                AdicionarErroProcessamento(notiticacao.Message);
            }

            return RespostaCustomizada();
        }

        protected bool OperacaoValida()
        {
            return _notifiable.IsValid();
        }

        protected void AdicionarErroProcessamento(string erro)
        {
            _notifiable.AddNotification(new ApplicationNotification(erro));
        }
    }
}