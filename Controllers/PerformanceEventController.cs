using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using WebApplicationOperaLublin.Interfaces.Services;

namespace WebApplicationOperaLublin.Controllers;

[ApiController]
[Route("[controller]")]
public class PerformanceEventController : ControllerBase
{
    private readonly IPerformanceEventService _performanceEventService;

    public PerformanceEventController(IPerformanceEventService performanceEventService)
    {
        _performanceEventService = performanceEventService;
    }

    [HttpGet("min-max-date")]
    public async Task<IActionResult> GetMinMaxDate()
    {
        return Ok(await _performanceEventService.GetMinMaxDate());
    }
    
    [HttpGet("by-month/{monthYear}")]
    public async Task<IActionResult> GetEventByMonth(string monthYear)
    {
        if (!DateTime.TryParseExact(monthYear, "yyyy-MM", CultureInfo.InvariantCulture,
                DateTimeStyles.None, out var monthStart))
            return BadRequest("Invalid monthYear format. Expected 'yyyy-MM'.");

        var start = new DateTime(monthStart.Year, monthStart.Month, 1, 0, 0, 0, DateTimeKind.Utc);
        var end = start.AddMonths(1);
        return Ok(await _performanceEventService.GetEventsByMonth(start, end));
    }
    
    [HttpGet("nearest-six")]
    public async Task<IActionResult> GetNearestSixPerformances()
    {
        return Ok(await _performanceEventService.GetNeatestSixPerformances());
    }

    [HttpGet("nearest-five-for-performance/{performanceId}")]
    public async Task<IActionResult> GetNearestFiveEventsForPerformance(int performanceId)
    {
        return Ok(await _performanceEventService.GetNearestFiveEventsForPerformanceAsync(performanceId));
    }
}