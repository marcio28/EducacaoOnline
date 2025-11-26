using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EducacaoOnline.Api.Extensions
{
    public class DeclaracaoAutorizanteAttribute : TypeFilterAttribute
    {
        public DeclaracaoAutorizanteAttribute(string nomeDeclaracao, string valorDeclaracao) : base(typeof(RequisitoDeclaracaoFilter))
        {
            Arguments = [new Claim(nomeDeclaracao, valorDeclaracao)];
        }
    }
}