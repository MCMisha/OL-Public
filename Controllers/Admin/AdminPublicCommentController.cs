using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplicationOperaLublin.Interfaces.Services.Admin;
using WebApplicationOperaLublin.Models;
using WebApplicationOperaLublin.Models.DTO;

namespace WebApplicationOperaLublin.Controllers.Admin;

[ApiController]
[Authorize]
[Route("[controller]")]
public class AdminPublicCommentController : ControllerBase
{
    private readonly IAdminPublicCommentService _service;

    public AdminPublicCommentController(IAdminPublicCommentService service)
    {
        _service = service;
    }

    [HttpGet("all")]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _service.GetAllAsync());
    }

    [HttpGet("by-id/{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var publicComment = await _service.GetByIdAsync(id);
        if (publicComment == null)
        {
            return NotFound();
        }
        return Ok(publicComment);
    }

    [HttpPost("create")]
    public async Task<IActionResult> Create([FromBody] PublicCommentCreateUpdateDto modelVm)
    {
        var publicComment = await _service.CreateAsync(modelVm);
        if (publicComment == null)
        {
            return BadRequest();
        }
        return Ok(publicComment);
    }
    
    [HttpPut("update")]
    public async Task<IActionResult> Update([FromBody] PublicCommentCreateUpdateDto modelVm)
    {
        var publicComment = await _service.UpdateAsync(modelVm);
        if (publicComment == null)
        {
            return BadRequest();
        }
        return Ok(publicComment);
    }

    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var publicComment = await _service.GetByIdAsync(id);
        if (publicComment == null)
        {
            return NotFound("Public comment not found");
        }
        await _service.DeleteAsync(id);
        return Ok();
    }
}