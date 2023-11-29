using Context.Entities.Event;
using ITentikaTest.WebAPI.Services.EventService;
using ITentikaTest.WebAPI.Settings;

namespace ITentikaTest.WebAPI.Services;

public class EventGeneratorService : BackgroundService
{
    private readonly IEventService eventService;
    private readonly EventGeneratorSettings settings;
    private readonly ILogger<EventGeneratorService> logger;
    private readonly Random random = new();

    public EventGeneratorService(IEventService eventService, EventGeneratorSettings settings, ILogger<EventGeneratorService> logger)
    {
        this.eventService = eventService;
        this.settings = settings;
        this.logger = logger;
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
        
        logger.Log(LogLevel.Trace, "Event {@event} generated", generatedEvent);

        await eventService.Publish(generatedEvent);
    }
}