using Microsoft.AspNetCore.Mvc;
using WebApplicationOperaLublin.Interfaces.Services;

namespace WebApplicationOperaLublin.Controllers;

[ApiController]
[Route("[controller]")]
public class MainPageBackgroundController : ControllerBase
{
    private readonly IMainPageBackgroundService _service;
    
    public MainPageBackgroundController(IMainPageBackgroundService service)
    {
        _service = service;
    }

    [HttpGet("all-active")]
    public async Task<IActionResult> GetAllActive()
    {
        return Ok(await _service.GetAllActiveMainPageBackgrounds());
    }
    
}