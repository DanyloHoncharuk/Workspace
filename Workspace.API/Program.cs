using Workspace.API.Middleware;
using Workspace.Application;
using Workspace.Application.Common;
using Workspace.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddControllers();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection(JwtSettings.SectionName));

var app = builder.Build();
app.UseExceptionHandler();

app.Run();
