using Context;
using Context.Entities.Incident;
using ITentikaTest.Common.Validators;
using Microsoft.EntityFrameworkCore;

namespace ITentikaTest.EventProcessor.Services.EventService;

public class EventService : IEventService
{
    private readonly IDbContextFactory<EventProcessorDbContext> dbContextFactory;
    private readonly IModelValidator<Incident> incidentValidator;

    public EventService(IDbContextFactory<EventProcessorDbContext> dbContextFactory, IModelValidator<Incident> incidentValidator)
    {
        this.dbContextFactory = dbContextFactory;
        this.incidentValidator = incidentValidator;
    }

    public async Task CreateIncident(Incident incident)
    {
        incidentValidator.Check(incident);

        var dbContext = await dbContextFactory.CreateDbContextAsync();

        await dbContext.Incidents.AddAsync(incident);

        dbContext.SaveChanges();
    }

    public async Task<IEnumerable<Incident>> GetIncidents(int offset = 0, int limit = 10)
    {
        var dbContext = await dbContextFactory.CreateDbContextAsync();

        var incidents = dbContext.Incidents.Include(x => x.Events)
            .Skip(Math.Max(offset, 0))
            .Take(Math.Min(limit, 1000));

        return await incidents.ToListAsync();
    }
}