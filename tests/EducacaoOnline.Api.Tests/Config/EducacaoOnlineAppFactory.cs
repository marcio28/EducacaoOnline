using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Hosting;

namespace EducacaoOnline.Api.Tests.Config
{
    public class EducacaoOnlineAppFactory<TProgram> : WebApplicationFactory<TProgram> where TProgram : class
    {
        protected override IHost CreateHost(IHostBuilder builder)
        {
            builder.UseEnvironment("Testing");
            return base.CreateHost(builder);
        }
    }
}
