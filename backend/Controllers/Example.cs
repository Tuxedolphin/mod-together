using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class ExampleController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok(new { message = "Hello from the backend!" });
    }
}
