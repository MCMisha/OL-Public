namespace WebApplicationOperaLublin.Models.DTO;

public class ImplementerCreateUpdateDto : Entity
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Role { get; set; } = null!;
    public bool IsDirector { get; set; } = false;
    public string? Photo { get; set; }
}