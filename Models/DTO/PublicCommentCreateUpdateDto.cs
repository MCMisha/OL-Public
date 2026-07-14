namespace WebApplicationOperaLublin.Models.DTO;

public class PublicCommentCreateUpdateDto : Entity
{
    public string Photo { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public int Stars { get; set; }
    public int PerformanceId { get; set; }
    public string Comment { get; set; } = null!;
    public DateTime DatePublished { get; set; }
    public bool IsShowing { get; set; } = false;
}