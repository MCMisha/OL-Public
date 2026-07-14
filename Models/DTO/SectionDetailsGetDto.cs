namespace WebApplicationOperaLublin.Models.DTO;

public class SectionDetailsGetDto : Entity
{
    public string Title { get; set; } = null!;
    public string Slug { get; set; } = null!;
    public string ContentHtml { get; set; } = "";
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public bool IsMain { get; set; } = false;
}