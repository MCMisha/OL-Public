using WebApplicationOperaLublin.Interfaces.Repositories.Admin;
using WebApplicationOperaLublin.Interfaces.Services.Admin;
using WebApplicationOperaLublin.Models;
using WebApplicationOperaLublin.Models.DTO;

namespace WebApplicationOperaLublin.Services.Admin;

public class AdminTicketPriceService : IAdminTicketPriceService
{
    private readonly IAdminTicketPriceRepository _ticketPriceRepository;

    public AdminTicketPriceService(IAdminTicketPriceRepository ticketPriceRepository)
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
    
    public async Task ReplacePriceGroupsAsync(int performanceId, IReadOnlyList<TicketPriceGroupCreateUpdateDto> groupsDto)
    {
        Validate(groupsDto);

        var entities = groupsDto.Select(g => new TicketPriceGroup
        {
            PerformanceId = performanceId,
            Name = g.Name.Trim(),
            SortOrder = g.SortOrder,
            Prices = g.Prices.Select(p => new TicketPrice
            {
                Type = p.Type,
                Amount = p.Amount
            }).ToList()
        }).ToList();

        await _ticketPriceRepository.ReplacePriceGroupsAsync(performanceId, entities);
    }
    
    private void Validate(IReadOnlyList<TicketPriceGroupCreateUpdateDto> groups)
    {
        foreach (var g in groups)
        {
            if (string.IsNullOrWhiteSpace(g.Name))
                throw new ArgumentException("Group name is required.");

            if (g.Prices is null || g.Prices.Count == 0)
                throw new ArgumentException($"Group '{g.Name}' must contain at least one price.");

            if (g.Prices.Any(p => p.Amount < 0))
                throw new ArgumentException($"Group '{g.Name}' contains negative amount.");
            
            if (g.Prices.GroupBy(p => p.Type).Any(x => x.Count() > 1))
                throw new ArgumentException($"Group '{g.Name}' has duplicated ticket types.");
        }
    }
}