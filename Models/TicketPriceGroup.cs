namespace WebApplicationOperaLublin.Models;

public class TicketPriceGroup
{
    public int Id { get; set; }

    public int PerformanceId { get; set; }
    public Performance Performance { get; set; } = null!;

    public string Name { get; set; } = null!; // "Sektor I", "I strefa"
    public int SortOrder { get; set; }

    public ICollection<TicketPrice> Prices { get; set; } = new List<TicketPrice>();
}