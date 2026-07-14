namespace WebApplicationOperaLublin.Models.DTO;

public class TicketPriceGroupCreateUpdateDto
{
    public string Name { get; set; }
    public int SortOrder { get; set; }
    public List<TicketPriceGetDto> Prices { get; set; } = [];
}