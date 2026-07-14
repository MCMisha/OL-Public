namespace WebApplicationOperaLublin.Models.DTO;

public class PerformanceEventCreateUpdateDto
{
    public int Id { get; set; } = 0;
    public DateTime EventDate { get; set; }
    public int PerformanceId { get; set; }
    public string BuyLink { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;
}