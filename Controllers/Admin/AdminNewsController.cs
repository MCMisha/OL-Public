using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplicationOperaLublin.Interfaces.Services.Admin;
using WebApplicationOperaLublin.Models;
using WebApplicationOperaLublin.Models.DTO;

namespace WebApplicationOperaLublin.Controllers.Admin;

[ApiController]
[Authorize]
[Route("[controller]")]
public class AdminNewsController : Controller
{
    private readonly ILogger<AdminNewsController> _logger;
    private readonly IAdminNewsService _adminNewsService;

    public AdminNewsController(ILogger<AdminNewsController> logger, IAdminNewsService adminNewsService)
    {
        _logger = logger;
        _adminNewsService = adminNewsService;
    }

    [HttpGet("all")]
    public async Task<IActionResult> GetAll()
    {
        var news = await _adminNewsService.GetAllNewsAsync();
        return Ok(news);
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateNews([FromBody] NewsCreateUpdateDto newsCreateUpdateDto)
    {
        var createdNews = await _adminNewsService.CreateNewsAsync(newsCreateUpdateDto);
        if (createdNews == null)
        {
            return BadRequest();
        }
        return Ok(createdNews);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetNews(int id)
    {
        var news = await _adminNewsService.GetNewsByIdAsync(id);
        if (news == null)
        {
            return NotFound();
        }
        return Ok(news);
    }

    [HttpPut("update")]
    public async Task<IActionResult> UpdateNews([FromBody] NewsCreateUpdateDto newsCreateUpdateDto)
    {
        var updatedNews = await _adminNewsService.UpdateNewsAsync(newsCreateUpdateDto);
        if (updatedNews == null)
        {
            return BadRequest();
        }

        return Ok(updatedNews);
    }

    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> DeleteNews(int id)
    {
        var result = await _adminNewsService.DeleteNewsAsync(id);
        if (result == false)
        {
            return NotFound();
        }
        return Ok(result);
    }
}