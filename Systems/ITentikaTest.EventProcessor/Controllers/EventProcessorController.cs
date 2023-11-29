using System.Net;
using Context.Entities.Event;
using Context.Entities.Incident;
using ITentikaTest.EventProcessor.Services.EventService;
using Microsoft.AspNetCore.Mvc;

namespace ITentikaTest.EventProcessor.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EventProcessorController : ControllerBase
{
    private readonly IEventService eventService;

    public EventProcessorController(IEventService eventService)
    {
        this.eventService = eventService;
    }

    /// <summary>
    /// Receive events endpoint
    /// </summary>
    /// <param name="generatedEvent">Received event</param>
    /// <returns>Received event</returns>
    [Route("add")]
    [HttpPost]
    [ProducesResponseType(typeof(Event), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> AddEvent([FromBody] Event generatedEvent)
    {
        await eventService.PushEvent(generatedEvent);
        return Ok(generatedEvent);
    }

    /// <summary>
    /// Get created incidents list ordered by incident creation time
    /// </summary>
    /// <param name="offset">Offset from the first incident</param>
    /// <param name="limit">Number of incidents to fetch</param>
    /// <returns></returns>
    [Route("Incidents")]
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<Incident>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetIncidents([FromQuery] int offset = 0, [FromQuery] int limit = 10)
    {
        var incidents = await eventService.GetIncidents(offset, limit);

        return Ok(incidents);
    }
}