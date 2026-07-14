using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplicationOperaLublin.Interfaces.Services.Admin;
using WebApplicationOperaLublin.Models.DTO;

namespace WebApplicationOperaLublin.Controllers.Admin;

[ApiController]
[Authorize]
[Route("[controller]")]
public class AdminArtistPhotoController : ControllerBase
{
    private readonly IAdminArtistPhotoService _artistPhotoService;

    public AdminArtistPhotoController(IAdminArtistPhotoService artistPhotoService)
    {
        _artistPhotoService = artistPhotoService;
    }

    [HttpGet("by-artist/{artistId:int}")]
    public async Task<IActionResult> GetArtistPhotos(int artistId)
    {
        var photos = await _artistPhotoService.GetArtistPhotosByArtistIdAsync(artistId);

        return Ok(photos);
    }

    [HttpGet("by-artist/{artistId:int}/by-id/{photoId:int}")]
    public async Task<IActionResult> GetArtistPhotoById(int artistId, int photoId)
    {
        var photo = await _artistPhotoService.GetArtistPhotoByIdAsync(photoId);

        if (photo == null || photo.ArtistId != artistId)
        {
            return NotFound();
        }

        return Ok(photo);
    }

    [HttpPost("create")]
    public async Task<IActionResult> AddPhotos(int artistId, [FromForm] CreatePhotosDto dto)
    {
        var createdPhotos = await _artistPhotoService.AddPhotosAsync(artistId, dto);

        if (createdPhotos == null)
        {
            return BadRequest("Nie dodano zdjęć. Sprawdź, czy pliki zostały przesłane poprawnie.");
        }

        return Ok(createdPhotos);
    }

    [HttpPut("update/{photoId:int}")]
    public async Task<IActionResult> UpdatePhoto(
        int artistId,
        int photoId,
        [FromForm] UpdatePhotoDto dto)
    {
        var updatedPhoto = await _artistPhotoService.UpdatePhotoAsync(artistId, photoId, dto);

        if (updatedPhoto == null)
        {
            return NotFound("Nie znaleziono zdjęcia albo przesłany plik jest pusty.");
        }

        return Ok(updatedPhoto);
    }

    [HttpDelete("delete/{photoId:int}")]
    public async Task<IActionResult> DeletePhoto(int artistId, int photoId)
    {
        var deleted = await _artistPhotoService.DeletePhotoAsync(artistId, photoId);

        if (!deleted)
        {
            return NotFound();
        }

        return NoContent();
    }
}