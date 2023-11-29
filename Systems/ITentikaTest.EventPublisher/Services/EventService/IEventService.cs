using Context.Entities.Event;

namespace ITentikaTest.WebAPI.Services.EventService;

public interface IEventService
{
    Task<bool> Publish(Event generatedEvent);
}