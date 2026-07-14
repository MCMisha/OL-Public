namespace WebApplicationOperaLublin.Models;

public class PublicComment : Entity
{
    public byte[] Photo { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public int Stars { get; set; }
    public int PerformanceId { get; set; }
    public Performance Performance { get; set; } = null!;
    public string Comment { get; set; } = null!;
    public DateTime DatePublished { get; set; }
    public bool IsShowing { get; set; }
}