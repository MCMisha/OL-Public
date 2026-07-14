using WebApplicationOperaLublin.Interfaces.Repositories;
using WebApplicationOperaLublin.Interfaces.Services;
using WebApplicationOperaLublin.Models.DTO;

namespace WebApplicationOperaLublin.Services;

public class TicketPriceService : ITicketPriceService
{
    private readonly ITicketPriceRepository _ticketPriceRepository;

    public TicketPriceService(ITicketPriceRepository ticketPriceRepository)
    {
        _ticketPriceRepository = ticketPriceRepository;
    }
    
    public async Task<IReadOnlyList<TicketPriceGroupCreateUpdateDto>> GetTicketPricesForPerformanceAsync(int performanceId)
    {
        var groups = await _ticketPriceRepository.GetGroupsWithPricesAsync(performanceId);

        return groups.Select(g => new TicketPriceGroupCreateUpdateDto{
            Name = g.Name,
            SortOrder = g.SortOrder,
            Prices = g.Prices
                .OrderBy(p => p.Type)
                .Select(p => new TicketPriceGetDto{Type = p.Type, Amount = p.Amount})
                .ToList()
        }).ToList();
    }
}