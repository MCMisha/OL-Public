namespace WebApplicationOperaLublin.Models.DTO;

public class NewsGetDto
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public required string Subtitle { get; set; }
    public required DateTime CreationDate { get; set; }
}