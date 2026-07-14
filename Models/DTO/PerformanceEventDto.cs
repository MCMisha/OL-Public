namespace WebApplicationOperaLublin.Models.DTO;

public sealed class PerformanceEventDto
{
    public int Id { get; set; }
    public DateTime StartAt { get; set; }
    public int PerformanceId { get; set; }
    public string Title { get; set; } = "";
    public string Genre { get; set; } = "";
    public string Place { get; set; } = "";
    public byte[] MainImage { get; set; } = [];
    public string BuyLink  { get; set; } = "";
    public bool IsActive { get; set; }
}