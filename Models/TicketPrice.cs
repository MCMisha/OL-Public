using WebApplicationOperaLublin.Models.Enums;

namespace WebApplicationOperaLublin.Models;

public class TicketPrice
{
    public int Id { get; set; }

    public int TicketPriceGroupId { get; set; }
    public TicketPriceGroup Group { get; set; } = null!;

    public TicketType Type { get; set; } // Normal / Discount
    public decimal Amount { get; set; }
}