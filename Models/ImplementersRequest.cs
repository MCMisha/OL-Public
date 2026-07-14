using WebApplicationOperaLublin.Models.DTO;

namespace WebApplicationOperaLublin.Models;

public class ImplementersRequest
{
    public IEnumerable<ImplementerCreateUpdateDto> Implementers { get; set; } = [];
}