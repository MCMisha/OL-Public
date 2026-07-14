using Microsoft.AspNetCore.Mvc;
using WebApplicationOperaLublin.Models;
using WebApplicationOperaLublin.Interfaces.Services;

namespace WebApplicationOperaLublin.Controllers;

[ApiController]
[Route("[controller]")]
public class NewsController : Controller
{
    private readonly INewsService _newsService;
    
    public NewsController(INewsService newsService)
    {
        _newsService = newsService;
    }

    [HttpGet("all")]
    public async Task<ActionResult<IEnumerable<News>>> GetAll()
    {
        var news = await _newsService.GetAllNews();
        return Ok(news);
    }

    [HttpGet("five-recent")]
    public async Task<ActionResult<IEnumerable<News>>> GetFiveRecent()
    {
        var news = await _newsService.GetFiveRecent();
        return Ok(news);
    }

    [HttpGet("by-id/{id}")]
    public async Task<ActionResult<News>> GetById(int id)
    {
        var newsById = await _newsService.GetNewsById(id);
        if (newsById == null)
        {
            return NotFound();
        }
        return Ok(newsById);
    }
}