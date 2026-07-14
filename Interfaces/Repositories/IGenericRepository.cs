using System.Linq.Expressions;
using WebApplicationOperaLublin.Models;

namespace WebApplicationOperaLublin.Interfaces.Repositories;

public interface IGenericRepository<T> where T : Entity
{
    Task<T?> GetByIdAsync(int id);
    Task<List<T>> GetByIdsAsync(IEnumerable<int> ids);
    Task<IEnumerable<T>> GetAllAsync();
    Task<T> CreateAsync(T entity);
    Task<T?> UpdateAsync(T entity);
    Task UpdateRangeAsync(IEnumerable<T> entities);

    Task<bool> DeleteAsync(int id);
}