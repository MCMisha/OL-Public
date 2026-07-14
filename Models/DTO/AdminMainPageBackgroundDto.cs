namespace WebApplicationOperaLublin.Models.DTO;

public class AdminMainPageBackgroundDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public int DisplayOrder { get; set; }
    public DateTime CreatedAt { get; set; }
    public required byte[] MainImage { get; set; }
}