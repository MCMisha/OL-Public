namespace WebApplicationOperaLublin.Models.DTO;

public class UpdateMainPageBackgroundDto
{
    public int Id { get; set; }
    public int PerformanceId { get; set; } 
    public string MainImage { get; set; } = null!;
    public string? ContentType { get; set; }
    public bool IsActive { get; set; }
    public int DisplayOrder { get; set; }
}