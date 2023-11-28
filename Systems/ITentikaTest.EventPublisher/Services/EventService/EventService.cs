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

    public EventService(IHttpClientFactory httpClientFactory)
    {
        this.httpClientFactory = httpClientFactory;
    }

    public async Task<HttpResponseMessage> Publish(Event generatedEvent)
    {
        var httpClient = httpClientFactory.CreateClient();

        var content = new StringContent(JsonSerializer.Serialize(generatedEvent),
            Encoding.UTF8,
            MediaTypeNames.Application.Json);

        return await httpClient.PostAsync(microserviceSettings.Uri + "/api/ProcessEvent", content);
    }
}