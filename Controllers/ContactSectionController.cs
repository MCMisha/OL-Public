using Microsoft.AspNetCore.Mvc;
using WebApplicationOperaLublin.Interfaces.Services;

namespace WebApplicationOperaLublin.Controllers;

[Route("[controller]")]
[ApiController]
public class ContactSectionController : ControllerBase
{
    private readonly IContactSectionService _service;

    public ContactSectionController(IContactSectionService service)
    {
        _service = service;
    }

    [HttpGet("all")]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _service.GetAllAsync());
    }

    [HttpGet("by-slug/{slug}")]
    public async Task<IActionResult> GetBySlug(string slug)
    {
        var item = await _service.GetBySlugAsync(slug);
        return item == null ? NotFound() : Ok(item);
    }
}