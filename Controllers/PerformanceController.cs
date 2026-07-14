using Microsoft.AspNetCore.Mvc;
using WebApplicationOperaLublin.Models;
using WebApplicationOperaLublin.Interfaces.Services;
using WebApplicationOperaLublin.Models.DTO;

namespace WebApplicationOperaLublin.Controllers;

[ApiController]
[Route("[controller]")]
public class PerformanceController : Controller
{
    private readonly ILogger<PerformanceController> _logger;
    private readonly IPerformanceService _performanceService;

    public PerformanceController(ILogger<PerformanceController> logger, IPerformanceService performanceService)
    {
        _logger = logger;
        _performanceService = performanceService;
    }
    
    [HttpGet("all")]
    public async Task<ActionResult<List<Performance>>> GetAll()
    {
        return Ok(await _performanceService.GetAllPerformances());
    }

    [HttpGet("all-for-about")]
    public async Task<ActionResult<List<PerformanceListAboutDto>>> GetAllForAbout()
    {
        return Ok(await _performanceService.GetAllPerformanceListAbout());
    }

    [HttpGet("by-id/{id}")]
    public async Task<ActionResult<Performance>> Get(int id)
    {
        var performance = await _performanceService.GetPerformanceById(id);
        if (performance == null)
        {
            return NotFound();
        }
        return Ok(performance);
    }

    [HttpGet("newest-premieres")]
    public async Task<ActionResult<List<Performance>>> GetNewestPremieres()
    {
        var newest = await _performanceService.GetNewestPerformancePremieres();
        return Ok(newest);
    }
}