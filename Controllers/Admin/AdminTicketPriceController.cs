using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplicationOperaLublin.Interfaces.Services.Admin;
using WebApplicationOperaLublin.Models.DTO;

namespace WebApplicationOperaLublin.Controllers.Admin;

[ApiController]
[Authorize]
[Route("[controller]")]
public class AdminTicketPriceController : ControllerBase
{
    private readonly IAdminTicketPriceService _adminTicketPriceService;

    public AdminTicketPriceController(IAdminTicketPriceService service)
    {
        _adminTicketPriceService = service;
    }
    
    [HttpGet("get-by-performance/{performanceId}")]
    public async Task<ActionResult<IReadOnlyList<TicketPriceGroupCreateUpdateDto>>> Get(int performanceId)
    {
        var result = await _adminTicketPriceService.GetTicketPricesForPerformanceAsync(performanceId);
        return Ok(result);
    }
    
    [HttpPut("update/{performanceId}")]
    public async Task<IActionResult> Replace(int performanceId, [FromBody] List<TicketPriceGroupCreateUpdateDto> groups)
    {
        await _adminTicketPriceService.ReplacePriceGroupsAsync(performanceId, groups);
        return NoContent();
    }
}