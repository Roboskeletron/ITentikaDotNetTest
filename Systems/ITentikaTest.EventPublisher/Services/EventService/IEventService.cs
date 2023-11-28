using Context.Entities.Event;

namespace ITentikaTest.WebAPI.Services.EventService;

public interface IEventService
{
    Task<HttpResponseMessage> Publish(Event generatedEvent);
}