using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplicationOperaLublin.Interfaces.Services.Admin;
using WebApplicationOperaLublin.Models.DTO;

namespace WebApplicationOperaLublin.Controllers.Admin;

[ApiController]
[Authorize]
[Route("[controller]")]
public class AdminPerformancePhotoController : ControllerBase
{
    private readonly IAdminPerformancePhotoService _service;

    public AdminPerformancePhotoController(IAdminPerformancePhotoService service)
    {
        _service = service;
    }

    [HttpGet("by-performance/{performanceId}")]
    public async Task<IActionResult> GetByPerformance(int performanceId)
    {
        var photos = await _service.GetPerformancePhotosByPerformanceIdAsync(performanceId);
        
        return Ok(photos);
    }
    
    [HttpGet("by-performance/{performanceId:int}/by-id/{photoId:int}")]
    public async Task<IActionResult> GetArtistPhotoById(int performanceId, int photoId)
    {
        var photo = await _service.GetPerformancePhotoByIdAsync(photoId);

        if (photo == null || photo.PerformanceId != performanceId)
        {
            return NotFound();
        }

        return Ok(photo);
    }
    
    [HttpPost("create/{performanceId:int}")]
    public async Task<IActionResult> AddPhotos(int performanceId, [FromForm] CreatePhotosDto dto)
    {
        var createdPhotos = await _service.AddPerformancePhotosAsync(performanceId, dto);

        if (createdPhotos == null)
        {
            return BadRequest("Nie dodano zdjęć. Sprawdź, czy pliki zostały przesłane poprawnie.");
        }

        return Ok(createdPhotos);
    }
    
    [HttpPut("update/{photoId:int}")]
    public async Task<IActionResult> UpdatePhoto(
        int performanceId,
        int photoId,
        [FromForm] UpdatePhotoDto dto)
    {
        var updatedPhoto = await _service.UpdatePerformancePhotoAsync(performanceId, photoId, dto);

        if (updatedPhoto == null)
        {
            return NotFound("Nie znaleziono zdjęcia albo przesłany plik jest pusty.");
        }

        return Ok(updatedPhoto);
    }

    [HttpDelete("delete/{photoId:int}")]
    public async Task<IActionResult> DeletePhoto(int performanceId, int photoId)
    {
        var deleted = await _service.DeletePerformancePhotoAsync(performanceId, photoId);

        if (!deleted)
        {
            return NotFound();
        }

        return NoContent();
    }
}