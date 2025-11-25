using EducacaoOnline.Api.Configuration;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables();

builder.Services
    .AddApiConfig()
    .AddApiVersioningConfig()
    .AddCorsConfig(builder.Configuration)
    .AddSwaggerConfiguration()
    .AddDbContextConfig(builder.Configuration)
    .AddIdentityConfig()
    .AddMediatR(config => config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()))
    .RegisterServices()
    .AddJwtConfig(builder.Configuration)
    .AddAutoMapper();

var app = builder.Build();

app.UseSwaggerConfiguration()
   .UseApiConfiguration(app.Environment);

app.UseDbMigrationHelper();

app.Run();

public partial class Program { }