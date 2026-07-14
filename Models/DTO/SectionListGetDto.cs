namespace WebApplicationOperaLublin.Models.DTO;

public class SectionListGetDto : Entity
{
    public string Title { get; set; } = null!;
    public string Slug { get; set; } = null!;
    public int Order { get; set; }
    public bool IsVisible { get; set; } = true;
    public bool IsMain { get; set; } = false;
}