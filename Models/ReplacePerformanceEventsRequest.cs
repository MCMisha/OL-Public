using WebApplicationOperaLublin.Models.DTO;

namespace WebApplicationOperaLublin.Models;

public class ReplacePerformanceEventsRequest
{
    public List<AdminPerformanceEventUpdateDto> Body { get; set; } = [];
}