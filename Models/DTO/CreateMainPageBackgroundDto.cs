namespace WebApplicationOperaLublin.Models.DTO;

public class CreateMainPageBackgroundDto
{
    public int PerformanceId { get; set; }
    public string MainImage { get; set; } = null!;
    public bool IsActive { get; set; } = true;
    public int DisplayOrder { get; set; }
}