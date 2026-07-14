namespace WebApplicationOperaLublin.Models.DTO;

public class CreatePhotosDto
{
    public List<IFormFile> Photos { get; set; } = new();
}