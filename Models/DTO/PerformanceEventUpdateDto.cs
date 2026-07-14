namespace WebApplicationOperaLublin.Models.DTO;

public sealed class PerformanceEventUpdateDto
{
    public int Id { get; set; }
    public DateTime StartAt { get; set; }
    public string BuyLink { get; set; } = "";
    public int PerformanceId { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
}