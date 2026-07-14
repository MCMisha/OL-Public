using WebApplicationOperaLublin.Models;

namespace WebApplicationOperaLublin.Interfaces.Repositories;

public interface ITicketPriceRepository
{
    Task<List<TicketPriceGroup>> GetGroupsWithPricesAsync(int performanceId);
}