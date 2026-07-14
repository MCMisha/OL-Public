using WebApplicationOperaLublin.Models.Enums;

namespace WebApplicationOperaLublin.Models;

public class Section : Entity
{
    public string Title { get; set; }
    public string Slug { get; set; }
    public int Order { get; set; }
    public bool IsVisible { get; set; } = true;
    public string ContentHtml { get; set; } = "";
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    public SectionType Type { get; set; } = SectionType.About;
    public bool IsMain { get; set; } = false;
}