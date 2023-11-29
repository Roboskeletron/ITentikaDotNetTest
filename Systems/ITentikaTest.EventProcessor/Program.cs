using Context;
using ITentikaTest.EventProcessor;
using ITentikaTest.EventProcessor.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.AddAppLogger();

var services = builder.Services;

services.AddAppServices();
services.AddHttpContextAccessor();
services.AddAppSwagger();
services.AddAppControllers();

var app = builder.Build();

app.UseAppSwagger();
app.UseAppMiddlewares();
app.UseAppControllers();

DbInitializer.Execute<EventProcessorDbContext>(app.Services);

app.Run();