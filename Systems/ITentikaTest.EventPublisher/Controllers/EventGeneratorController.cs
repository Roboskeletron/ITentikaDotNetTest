using System.ComponentModel.DataAnnotations;
using System.Net;
using Context.Entities.Event;
using ITentikaTest.Common.Responses;
using ITentikaTest.WebAPI.Models;
using ITentikaTest.WebAPI.Services.EventService;
using Microsoft.AspNetCore.Mvc;

namespace ITentikaTest.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EventGeneratorController : ControllerBase
{
    private readonly IEventService eventService;

    public EventGeneratorController(IEventService eventService)
    {
        this.eventService = eventService;
    }

    /// <summary>
    /// Manually generate new event
    /// </summary>
    /// <param name="eventType">Type of event</param>
    /// <returns>Generated event and its send status</returns>
    [Route("")]
    [HttpPost]
    [ProducesResponseType(typeof(GeneratedEventModel), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> GenerateEvent([FromQuery] [Required] EventTypeEnum eventType)
    {
        var generatedEvent = new Event
        {
            Type = eventType
        };

        var result = await eventService.Publish(generatedEvent);
        
        return Ok(new GeneratedEventModel
            {
                GeneratedEvent = generatedEvent,
                IsSent = result
            })
            ;
    }
}