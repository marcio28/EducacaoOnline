using EducacaoOnline.Api.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("appsettings.json", true, true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", true, true)
    .AddEnvironmentVariables();

builder.Services
    .AddApiConfig()
    .AddApiVersioningConfig()
    .AddCorsConfig(builder.Configuration)
    .AddSwaggerConfiguration()
    .AddDbContextConfig(builder.Configuration)
    .AddIdentityConfig()
    .RegisterServices()
    .AddJwtConfig(builder.Configuration);

var app = builder.Build();

app.UseSwaggerConfiguration()
   .UseApiConfiguration(app.Environment);

app.UseDbMigrationHelper();

app.Run();

public partial class Program { }