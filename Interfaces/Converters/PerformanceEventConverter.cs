using WebApplicationOperaLublin.Interfaces.Converters;
using WebApplicationOperaLublin.Models;
using WebApplicationOperaLublin.Models.DTO;

namespace WebApplicationOperaLublin.Converters;

public class PerformanceEventConverter : IPerformanceEventConverter
{
    public PerformanceEventDto ConvertFromPerformanceEventToPerformanceEventDto(PerformanceEvent model)
    {
        return new PerformanceEventDto
        {
            Id = model.Id,
            Title = model.Performance.Title,
            StartAt = model.StartAt,
            Genre = model.Performance.GenreNavigation.Name,
            PerformanceId = model.PerformanceId,
            Place = model.Performance.PlaceNavigation.Name,
        };
    }
}