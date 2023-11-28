using Microsoft.AspNetCore.Mvc;

namespace ITentikaTest.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TestController : ControllerBase
{
    /// <summary>
    /// Get welcome string
    /// </summary>
    /// <returns>string</returns>
    [HttpGet]
    [Route("")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
    public IActionResult HelloApi()
    {
        return Ok("Hello form API!");
    }
}