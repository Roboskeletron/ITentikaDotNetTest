using Context.Entities.Event;
using Microsoft.AspNetCore.Mvc;

namespace ITentikaTest.EventProcessor.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class EventProcessorController : ControllerBase
{
    [HttpPost]
    [Route("/add")]
    public async Task<IActionResult> AddEvent([FromBody] Event generatedEvent)
    {
        return Ok(generatedEvent);
    }
}