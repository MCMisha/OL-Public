namespace WebApplicationOperaLublin.Models.DTO;

public class PublicCommentDto
{
    public byte[] Photo { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public int Stars { get; set; }

    public string PerformanceTitle { get; set; } = null!;

    public string Comment { get; set; } = null!;
    public DateTime DatePublished { get; set; }
}