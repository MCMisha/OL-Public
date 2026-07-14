namespace WebApplicationOperaLublin.Models.DTO;

public sealed class AdminPerformanceEventListDto
{
    public int Id { get; set; }
    public DateTime StartAt { get; set; }
    public string BuyLink { get; set; } = "";
    public int PerformanceId { get; set; }
    public bool IsActive { get; set; } = true;
    public string PerformanceTitle { get; set; } = "";
    public string GenreName { get; set; } = "";
    public string PlaceName { get; set; } = "";
}