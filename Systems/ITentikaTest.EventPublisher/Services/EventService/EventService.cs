using System.Net.Mime;
using System.Text;
using System.Text.Json;
using Context.Entities.Event;
using ITentikaTest.Common.Settings;

namespace ITentikaTest.WebAPI.Services.EventService;

public class EventService : IEventService
{
    private readonly IHttpClientFactory httpClientFactory;
    private readonly MicroserviceSettings microserviceSettings;
    private readonly ILogger<EventService> logger;

    public EventService(IHttpClientFactory httpClientFactory, MicroserviceSettings microserviceSettings, ILogger<EventService> logger)
    {
        this.httpClientFactory = httpClientFactory;
        this.microserviceSettings = microserviceSettings;
        this.logger = logger;
    }

    public async Task<bool> Publish(Event generatedEvent)
    {
        var httpClient = httpClientFactory.CreateClient();

        var content = new StringContent(JsonSerializer.Serialize(generatedEvent),
            Encoding.UTF8,
            MediaTypeNames.Application.Json);

        logger.Log(LogLevel.Information, "Send event {@event} to {@uri}",  generatedEvent, microserviceSettings.Uri);

        bool result = false;
        
        try
        {
            await httpClient.PostAsync(microserviceSettings.Uri, content);
            result = true;
        }
        catch (HttpRequestException requestException)
        {
            logger.LogError(requestException, "Unable to reach {@url}", microserviceSettings.Uri);
        }

        return result;
    }
}