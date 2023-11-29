using Context.Entities.Event;
using ITentikaTest.EventProcessor.Services.EventService;
using Microsoft.AspNetCore.Mvc;

namespace ITentikaTest.EventProcessor.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class EventProcessorController : ControllerBase
{
    private readonly IEventService eventService;

    public EventProcessorController(IEventService eventService)
    {
        this.eventService = eventService;
    }

    [HttpPost]
    [Route("/add")]
    public async Task<IActionResult> AddEvent([FromBody] Event generatedEvent)
    {
        await eventService.PushEvent(generatedEvent);
        return Ok(generatedEvent);
    }
}