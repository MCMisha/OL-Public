namespace WebApplicationOperaLublin.Models.DTO;

public class PerformanceCreateUpdateDto : Entity
{
    public string Title { get; set; } = null!;
    public int Genre { get; set; }
    public int Place { get; set; }
    public string Duration { get; set; } = null!;
    public int BreaksCount { get; set; }
    public string Description { get; set; } = null!;
    public string MainImage { get; set; } = null!;
    public string? Poster { get; set; } = null;
    public DateTime? PremiereDate { get; set; } = null!;
}