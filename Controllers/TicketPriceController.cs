using Microsoft.AspNetCore.Mvc;
using WebApplicationOperaLublin.Interfaces.Services;
using WebApplicationOperaLublin.Models.DTO;

namespace WebApplicationOperaLublin.Controllers;

[ApiController]
[Route("[controller]")]
public class TicketPriceController : ControllerBase
{
    private readonly ITicketPriceService _ticketPriceService;

    public TicketPriceController(ITicketPriceService ticketPriceService)
    {
        _ticketPriceService = ticketPriceService;
    }
    
    [HttpGet("get-by-performance/{performanceId}")]
    public async Task<ActionResult<IReadOnlyList<TicketPriceGroupCreateUpdateDto>>> Get(int performanceId)
    {
        var result = await _ticketPriceService.GetTicketPricesForPerformanceAsync(performanceId);
        return Ok(result);
    }
    
}