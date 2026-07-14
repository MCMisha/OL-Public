using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplicationOperaLublin.Interfaces.Services;

namespace WebApplicationOperaLublin.Controllers;

[ApiController]
[Route("[controller]/{performanceId}")]
public class PerformanceCastController : ControllerBase
{
    private readonly ICastService _castService;

    public PerformanceCastController(ICastService castService)
    {
        _castService = castService;
    }

    [HttpGet("get-cast")]
    public async Task<IActionResult> GetCast(int performanceId)
    {
        return Ok(await _castService.GetCast(performanceId));
    }
}