using WebApplicationOperaLublin.Models.DTO;

namespace WebApplicationOperaLublin.Interfaces.Services.Admin;

public interface IAdminTicketPriceService
{
    Task<IReadOnlyList<TicketPriceGroupCreateUpdateDto>> GetTicketPricesForPerformanceAsync(int performanceId);
    Task ReplacePriceGroupsAsync(int performanceId, IReadOnlyList<TicketPriceGroupCreateUpdateDto> groups);
}