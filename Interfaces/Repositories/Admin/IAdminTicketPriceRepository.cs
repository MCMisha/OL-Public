using WebApplicationOperaLublin.Models;

namespace WebApplicationOperaLublin.Interfaces.Repositories.Admin;

public interface IAdminTicketPriceRepository
{
    Task<List<TicketPriceGroup>> GetGroupsWithPricesAsync(int performanceId);
    Task ReplacePriceGroupsAsync(int performanceId, List<TicketPriceGroup> newGroups);
}