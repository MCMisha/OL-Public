using System.Text;
using Microsoft.IdentityModel.Tokens;
using WebApplicationOperaLublin.Interfaces.Converters;
using WebApplicationOperaLublin.Models;
using WebApplicationOperaLublin.Models.DTO;

namespace WebApplicationOperaLublin.Converters;

public class ImplementerConverter : IImplementerConverter
{
    public ImplementerCreateUpdateDto ConvertFromModelToViewModel(Implementer model)
    {
        return new ImplementerCreateUpdateDto
        {
            Id = model.Id,
            FirstName = model.FirstName,
            LastName = model.LastName,
            Role = model.Role,
            IsDirector = model.IsDirector,
            Photo = model.Photo != null
                ? Convert.ToBase64String(model.Photo)
                : null,
        };
    }

    public Implementer ConvertFromViewModelToModel(ImplementerCreateUpdateDto createUpdateDto)
    {
        return new Implementer
        {
            Id = createUpdateDto.Id,
            FirstName = createUpdateDto.FirstName,
            LastName = createUpdateDto.LastName,
            Role = createUpdateDto.Role,
            IsDirector = createUpdateDto.IsDirector,
            Photo = string.IsNullOrEmpty(createUpdateDto.Photo)
                ? null
                : Convert.FromBase64String(createUpdateDto.Photo)
        };
    }
}