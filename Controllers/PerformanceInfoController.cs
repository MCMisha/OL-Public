using Microsoft.AspNetCore.Mvc;
using WebApplicationOperaLublin.Interfaces.Services;

namespace WebApplicationOperaLublin.Controllers;

[ApiController]
[Route("[controller]")]
public class PerformanceInfoController : Controller
{
    private readonly IPerformanceInfoService _performanceInfoService;

    public PerformanceInfoController(IPerformanceInfoService performanceInfoService)
    {
        _performanceInfoService = performanceInfoService;
    }

    [HttpGet("{performanceId:int}/implementers")]
    public async Task<IActionResult> GetImplementersByPerformance([FromRoute] int performanceId)
    {
        var result = await _performanceInfoService.GetImplementersByPerformance(performanceId);
        return Ok(result);
    }
}