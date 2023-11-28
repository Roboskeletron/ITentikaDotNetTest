using System.Net.Mime;
using Context.Entities.Event;
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

    [Route("")]
    [HttpPost]
    public async Task GenerateEvent([FromQuery] EventTypeEnum eventType)
    {
        var generatedEvent = new Event
        {
            Type = eventType
        };

        var response = eventService.Publish(generatedEvent);
        HttpContext.Response.ContentType = MediaTypeNames.Application.Json;
        HttpContext.Response.StatusCode = (int)response.Result.StatusCode;

        await response.Result.Content.CopyToAsync(HttpContext.Response.Body);
    }
}