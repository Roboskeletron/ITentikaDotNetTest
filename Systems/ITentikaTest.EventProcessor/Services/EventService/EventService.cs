using Castle.Core.Internal;
using Context;
using Context.Entities.Event;
using Context.Entities.Incident;
using ITentikaTest.Common.Validators;
using Microsoft.EntityFrameworkCore;

namespace ITentikaTest.EventProcessor.Services.EventService;

public class EventService : IEventService
{
    private readonly IDbContextFactory<EventProcessorDbContext> dbContextFactory;
    private readonly ILogger<EventService> logger;
    private readonly Queue<Event> eventQueue = new();
    private readonly IModelValidator<Event> eventValidator;

    public EventService(IDbContextFactory<EventProcessorDbContext> dbContextFactory, ILogger<EventService> logger, IModelValidator<Event> eventValidator)
    {
        this.dbContextFactory = dbContextFactory;
        this.logger = logger;
        this.eventValidator = eventValidator;
    }

    public async Task CreateIncident(Incident incident)
    {
        var dbContext = await dbContextFactory.CreateDbContextAsync();

        try
        {
            await dbContext.Incidents.AddAsync(incident);

            dbContext.SaveChanges();

            logger.LogInformation("Incident {@incident} created", incident);
        }
        catch (DbUpdateException exception)
        {
            logger.LogError(exception, "Unable to save incident {@incident}", incident);
        }
    }

    public async Task<IEnumerable<Incident>> GetIncidents(int offset = 0, int limit = 10)
    {
        var dbContext = await dbContextFactory.CreateDbContextAsync();

        var incidents = dbContext.Incidents.Include(x => x.Events)
            .OrderBy(incident => incident.Time)
            .Skip(Math.Max(offset, 0))
            .Take(Math.Min(limit, 1000));

        return await incidents.ToListAsync();
    }

    public Task PushEvent(Event generatedEvent)
    {
        eventValidator.Check(generatedEvent);
        
        eventQueue.Enqueue(generatedEvent);

        return Task.CompletedTask;
    }

    public async Task<Event>? FetchEvent()
    {
        if (eventQueue.IsNullOrEmpty())
        {
            return null;
        }

        return eventQueue.Dequeue();
    }
}