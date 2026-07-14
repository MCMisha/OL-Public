using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplicationOperaLublin.Interfaces.Services.Admin;
using WebApplicationOperaLublin.Models;
using WebApplicationOperaLublin.Models.DTO;

namespace WebApplicationOperaLublin.Controllers.Admin;

[ApiController]
[Route("[controller]")]
[Authorize]
public class AdminContactSectionController : ControllerBase
{
    private readonly IAdminContactSectionService _service;

    public AdminContactSectionController(IAdminContactSectionService service)
    {
        _service = service;
    }
    
    [HttpPost("create")]
    public async Task<IActionResult> Create([FromBody] AboutSectionCreateDto model)
    {
        var aboutSection = await _service.CreateAsync(model);
        return Ok(aboutSection);
    }

    [HttpGet("all")]
    public async Task<IActionResult> GetAll()
    {
        IEnumerable<SectionListGetDto> aboutSections = await _service.GetAllAsync();
        return Ok(aboutSections);
    }

    [HttpGet("by-id/{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var item = await _service.GetByIdAsync(id);
        if (item == null)
        {
            return NotFound();
        }
        return Ok(item);
    }

    [HttpGet("{slug}")]
    public async Task<IActionResult> GetBySlugAsync(string slug)
    {
        SectionDetailsGetDto? aboutSection = await _service.GetBySlugAsync(slug);
        if (aboutSection == null)
        {
            return NotFound();
        }
        return Ok(aboutSection);
    }

    [HttpPut("update")]
    public async Task<IActionResult> Update([FromBody] Section model)
    {
        var aboutSection = await _service.UpdateAsync(model);
        if (aboutSection == null)
        {
            return BadRequest();
        }
        return Ok(aboutSection);
    }
    
    [HttpPut("update-order")]
    public async Task<IActionResult> UpdateOrder([FromBody] List<UpdateOrderRequest> model)
    {
        await _service.UpdateOrderAsync(model);
        return Ok();
    }

    [HttpPut("update-main")]
    public async Task<IActionResult> UpdateMain(int id)
    {
        var section = await _service.SetMainAsync(id);
        return Ok(section);
    }
    
    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var res = await _service.DeleteAsync(id);
        if (res)
        {
            return Ok(res);
        }
        return BadRequest();
    }
}