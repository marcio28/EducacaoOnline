using EducacaoOnline.Core.Messages.ApplicationNotifications;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Net;

namespace EducacaoOnline.Api.Controllers
{
    [ApiController]
    public class MainController : Controller
    {
        private readonly INotifiable _notifiable;

        protected MainController(INotifiable notifiable)
        {
            _notifiable = notifiable;
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
                var errorMsg = erro.Exception == null ? erro.ErrorMessage : erro.Exception.Message;
                AdicionarErroProcessamento(errorMsg);
            }
            return RespostaCustomizada();
        }

        protected ActionResult RespostaCustomizada(List<ApplicationNotification> notifications)
        {
            foreach (var notitication in notifications)
            {
                AdicionarErroProcessamento(notitication.Message);
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