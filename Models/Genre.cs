using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplicationOperaLublin.Models;

[Table("genre")]
public class Genre : Entity
{
    [Required(ErrorMessage = "Nazwa gatunku jest wymagana")]
    [StringLength(50, MinimumLength = 3,
        ErrorMessage = "Długość nazwy gatunku musi być nie dłużej niż 50 i nie mniej niż 3.")]
    [DataType(DataType.Text)]
    public string Name { get; set; } = null!;

    public virtual ICollection<Performance> Performances { get; set; } = new List<Performance>();
}