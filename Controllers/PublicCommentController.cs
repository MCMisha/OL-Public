using Microsoft.AspNetCore.Mvc;
using WebApplicationOperaLublin.Interfaces.Services;
using WebApplicationOperaLublin.Models;
using WebApplicationOperaLublin.Models.DTO;

namespace WebApplicationOperaLublin.Controllers;

[ApiController]
[Route("[controller]")]
public class PublicCommentController : ControllerBase
{
    private readonly IPublicCommentService _service;

    public PublicCommentController(IPublicCommentService service)
    {
        _service = service;
    }

    [HttpGet("all")]
    public async Task<ActionResult<IEnumerable<PublicCommentDto>>> GetAllShowingAsync()
    {
        return Ok(await _service.GetAllShowingAsync());
    }
}