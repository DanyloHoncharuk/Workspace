using Workspace.Application;
using Workspace.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();
app.MapGet("/", () => "Hello World!");

app.Run();
