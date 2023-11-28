using ITentikaTest.EventProcessor.Configuration;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;

services.AddAppSwagger();
services.AddAppControllers();

var app = builder.Build();

app.UseAppSwagger();
app.UseAppMiddlewares();
app.UseAppControllers();

app.Run();