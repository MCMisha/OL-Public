namespace WebApplicationOperaLublin.Models.DTO;

public class MainPageBackgroundDto
{
    public int Id { get; set; }

    public int PerformanceId { get; set; }

    public string PerformanceTitle { get; set; } = null!;

    public string Genre { get; set; } = null!;

    public byte[] MainImage { get; set; } = null!;

    public List<DateTime> NearestEvents { get; set; } = [];
}