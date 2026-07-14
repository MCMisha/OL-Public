namespace WebApplicationOperaLublin.Models.DTO;

public class NewsCreateUpdateDto : Entity
{
    public required string Title { get; set; }
    public required string Subtitle { get; set; }
    public required string MainImage { get; set; }
    public DateTime CreationDate { get; set; }
    public required string Content { get; set; }
    public required int Category { get; set; }
}