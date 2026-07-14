namespace WebApplicationOperaLublin.Models.DTO;

public class PerformancePhotoDto
{
    public int Id { get; set; }

    public int PerformanceId { get; set; }

    public string Photo { get; set; } = string.Empty;
}