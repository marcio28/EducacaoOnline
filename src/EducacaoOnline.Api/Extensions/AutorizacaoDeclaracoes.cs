namespace EducacaoOnline.Api.Extensions
{
    public class AutorizacaoDeclaracoes
    {
        public static bool ValidarDeclaracoesUsuario(HttpContext context, string nomeDeclaracao, string valorDeclaracao)
        {
            if (context.User.Identity is null) throw new InvalidOperationException();

            return context.User.Identity.IsAuthenticated &&
                   context.User.Claims.Any(d => d.Type == nomeDeclaracao && d.Value.Split(',').Contains(valorDeclaracao));
        }
    }
}