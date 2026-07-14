using WebApplicationOperaLublin.Models.Enums;

namespace WebApplicationOperaLublin.Models.DTO;

public class TicketPriceGetDto
{
    public TicketType Type { get; set; }
    public decimal Amount { get; set; }
}