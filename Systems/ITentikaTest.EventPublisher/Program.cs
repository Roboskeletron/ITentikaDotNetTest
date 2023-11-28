using ITentikaTest.WebAPI;
using ITentikaTest.WebAPI.Configuration;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;

services.AddAppServices();
services.AddAppSwagger();
services.AddAppControllers();

var app = builder.Build();

app.UseAppSwagger();

app.UseAppControllers();

app.UseAppMiddlewares();

app.Run();