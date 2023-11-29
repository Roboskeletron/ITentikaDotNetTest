namespace ITentikaTest.WebAPI.Configuration;

public static class SwaggerConfiguration
{
    public static IServiceCollection AddAppSwagger(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(options =>
        {
            const string xmlFile = "api.xml";
            var filePath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            options.IncludeXmlComments(filePath);
        });
        
        return services;
    }

    public static void UseAppSwagger(this WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }
}