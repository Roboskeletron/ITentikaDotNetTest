using Context.Entities.Event;
using ITentikaTest.WebAPI.Services.EventService;
using ITentikaTest.WebAPI.Settings;

namespace ITentikaTest.WebAPI.Services;

public class EventGeneratorService : BackgroundService
{
    private readonly IEventService eventService;
    private readonly EventGeneratorSettings settings;
    private readonly Random random = new();

    public EventGeneratorService(IEventService eventService, EventGeneratorSettings settings)
    {
        this.eventService = eventService;
        this.settings = settings;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var delay = random.Next(settings.MaxDelay + 1);

            await Work();
            
            await Task.Delay(delay, stoppingToken);
        }
    }

    private async Task Work()
    {
        var eventType = (EventTypeEnum)random.Next(1, 5);
        var generatedEvent = new Event
        {
            Type = eventType
        };

        await eventService.Publish(generatedEvent);
    }
}