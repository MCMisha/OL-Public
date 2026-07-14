namespace WebApplicationOperaLublin.Models.DTO;

public sealed class AdminPerformanceEventUpdateDto
{
    public DateTime StartAt { get; set; }     // ISO string -> DateTime
    public string? BuyLink { get; set; }
    public bool IsActive { get; set; }
}