using WebApplicationOperaLublin.Models.DTO;

namespace WebApplicationOperaLublin.Interfaces.Services;

public interface ITicketPriceService
{
    Task<IReadOnlyList<TicketPriceGroupCreateUpdateDto>> GetTicketPricesForPerformanceAsync(int performanceId);
}