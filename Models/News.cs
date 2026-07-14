using System.ComponentModel.DataAnnotations.Schema;
using WebApplicationOperaLublin.Models.Enums;

namespace WebApplicationOperaLublin.Models;

[Table("news")]
public class News : Entity
{
    public string Title { get; set; }
    public string Subtitle { get; set; }
    public byte[] MainImage { get; set; }
    public DateTime CreationDate { get; set; }
    public string Content { get; set; }
    public NewsCategory Category { get; set; } = NewsCategory.Komunikaty;
}