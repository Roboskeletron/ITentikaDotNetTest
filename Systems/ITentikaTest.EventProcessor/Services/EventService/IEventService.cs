using Context.Entities.Event;
using Context.Entities.Incident;

namespace ITentikaTest.EventProcessor.Services.EventService;

public interface IEventService
{
    Task CreateIncident(Incident incident);
    Task<IEnumerable<Incident>> GetIncidents(int offset = 0, int limit = 10);
    Task PushEvent(Event generatedEvent);
    Task<Event>? FetchEvent();
}