using Castle.Core.Internal;
using Context.Entities.Event;
using ITentikaTest.EventProcessor.Services.EventService;
using ITentikaTest.EventProcessor.Services.Facories;

namespace ITentikaTest.EventProcessor.Services;

public class EventProcessorService : BackgroundService
{
    private readonly IEventService eventService;
    private readonly IIncidentFactory incidentFactory;

    public EventProcessorService(IEventService eventService, IIncidentFactory incidentFactory)
    {
        this.eventService = eventService;
        this.incidentFactory = incidentFactory;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        incidentFactory.IncidentBuildCompleted += IncidentFactoryOnIncidentBuildCompleted;
        while (!stoppingToken.IsCancellationRequested)
        {
            await Work();
            await Task.Delay(10, stoppingToken);
        }
    }

    private async void IncidentFactoryOnIncidentBuildCompleted(object? sender, IncidentEventArgs e)
    {
        await eventService.CreateIncident(e.Incident);
    }

    private async Task Work()
    {
        var processingEvent = await eventService.FetchEvent();

        if (processingEvent is null)
        {
            return;
        }
        
        await ProcessEvent(processingEvent);
    }

    private Task ProcessEvent(Event processingEvent)
    {
        incidentFactory.PushEvent(processingEvent);
        
        return Task.CompletedTask;
    }
}