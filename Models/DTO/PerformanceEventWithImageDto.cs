namespace WebApplicationOperaLublin.Models.DTO;

public sealed class PerformanceEventWithImageDto
{
    public int EventId { get; set; }
    public DateTime StartAt { get; set; }
    public int PerformanceId { get; set; }
    public string Title { get; set; } = "";
    public string Genre { get; set; } = "";
    public string Place { get; set; } = "";
    public string BuyLink { get; set; } = "";
    public bool IsActive { get; set; }
    public byte[] MainImage { get; set; } = null!;
}