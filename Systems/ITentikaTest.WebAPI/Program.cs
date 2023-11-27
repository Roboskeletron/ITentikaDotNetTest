using ITentikaTest.WebAPI.Configuration;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;

services.AddAppSwagger();

var app = builder.Build();

app.UseAppSwagger();

app.Run();