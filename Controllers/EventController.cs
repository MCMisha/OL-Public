using Microsoft.AspNetCore.Mvc;
using WebApplicationOperaLublin.Interfaces.Services;
using WebApplicationOperaLublin.Models;

namespace WebApplicationOperaLublin.Controllers;

[Obsolete("Not used any more", true)]
[ApiController]
[Route("[controller]")]
public class EventController : Controller
{
    private readonly ILogger<EventController> _logger;
    private readonly IEventDetailsService _eventDetailsService;
    
    public EventController(ILogger<EventController> logger, IEventDetailsService eventDetailsService)
    {
        _logger = logger;
        _eventDetailsService = eventDetailsService;
    }
    
    [HttpGet("{id}/dates")]
    public async Task<IActionResult> GetEventDates(int id)
    {
        var dates = await _eventDetailsService.GetDatesForPerformanceAsync(id);
        if (!dates.Any())
        {
            return NotFound();
        }
        return Ok(dates);
    }

    [HttpGet("{yearMonth}")]
    public async Task<IActionResult> GetEventsForMonth(string yearMonth)
    {
        var events = await _eventDetailsService.GetEventDatesAsync(yearMonth);
        if (!events.Any())
        {
            return NotFound();
        }
        return Ok(events);
    }

    [HttpGet("minmaxdates")]
    public async Task<IActionResult> GetMinMaxDates()
    {
        var minMaxDate = await _eventDetailsService.GetMinMaxDateAsync();
        if (minMaxDate == null)
        {
            return NotFound();
        }
        return Ok(minMaxDate);
    }
}