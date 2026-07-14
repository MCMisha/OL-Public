namespace WebApplicationOperaLublin.Models.DTO;

public sealed class PerformanceListAboutDto
{ 
    public int Id { get; set; }
    public required string Title { get; set; }
    public required byte[] MainImage { get; set; }
    public required string Genre { get; set; }
    public int BreaksCount { get; set; }
    public DateTime? PremiereDate { get; set; }
    public string? DirectorName { get; set; }
    public required IEnumerable<DateTime> NearestEvents { get; set; }
}