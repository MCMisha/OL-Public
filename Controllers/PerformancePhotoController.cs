using Microsoft.AspNetCore.Mvc;
using WebApplicationOperaLublin.Interfaces.Services.Admin;

namespace WebApplicationOperaLublin.Controllers;

[ApiController]
[Route("[controller]")]
public class PerformancePhotoController : ControllerBase
{
    private readonly IAdminPerformancePhotoService _service;

    public PerformancePhotoController(IAdminPerformancePhotoService service)
    {
        _service = service;
    }
    
    [HttpGet("by-performance/{performanceId}")]
    public async Task<IActionResult> GetByPerformance(int performanceId)
    {
        var photos = await _service.GetPerformancePhotosByPerformanceIdAsync(performanceId);
        
        return Ok(photos);
    }
}