using Microsoft.EntityFrameworkCore;
using WebApplicationOperaLublin.Contexts;
using WebApplicationOperaLublin.Interfaces.Repositories.Admin;
using WebApplicationOperaLublin.Models;

namespace WebApplicationOperaLublin.Repositories.Admin;

public class AdminPerformanceInfoRepository : IAdminPerformanceInfoRepository
{
    private readonly AppDbContext _context;

    public AdminPerformanceInfoRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Implementer>> AddImplementers(IEnumerable<Implementer> implementers,
        int performanceId)
    {
        var addImplementers = implementers.ToList();
        var tasks = addImplementers.Select(implementer =>
            _context.Implementers.AddAsync(implementer).AsTask());
        await Task.WhenAll(tasks);
        await _context.SaveChangesAsync();

        var addRelationPerformanceImplementer = addImplementers.Select(implementer =>
            _context.PerformanceImplementers.AddAsync(new PerformanceImplementer
            {
                ImplementerId = implementer.Id,
                PerformanceId = performanceId
            }).AsTask());
        await Task.WhenAll(addRelationPerformanceImplementer);
        await _context.SaveChangesAsync();
        return addImplementers;
    }

    public async Task<IEnumerable<Implementer>> GetImplementersByPerformance(int performanceId)
    {
        return await _context.PerformanceImplementers
            .Where(pi => pi.PerformanceId == performanceId)
            .Include(pi => pi.Implementer)
            .Select(pi => pi.Implementer)
            .ToListAsync();
    }

    public async Task<Implementer?> GetImplementerById(int performanceId, int implementerId)
    {
        return await _context.PerformanceImplementers
            .Where(pi => pi.PerformanceId == performanceId && pi.ImplementerId == implementerId)
            .Include(pi => pi.Implementer)
            .Select(pi => pi.Implementer)
            .FirstOrDefaultAsync();
    }

    public async Task<Implementer> UpdateImplementer(int performanceId, int implementerId, Implementer implementer)
    {
        var existing = await _context.PerformanceImplementers
            .FirstOrDefaultAsync(x =>
                x.PerformanceId == performanceId &&
                x.ImplementerId == implementerId);

        if (existing == null)
        {
            throw new KeyNotFoundException(
                $"Implementer {implementerId} for performance {performanceId} not found.");
        }

        var existingImplementer = await _context.Implementers.FirstAsync(impl => impl.Id == implementerId);
        _context.Entry(existingImplementer).CurrentValues.SetValues(implementer);
        
        await _context.SaveChangesAsync();

        return implementer; 
    }

    public async Task<bool> DeleteImplementer(int performanceId, int implementerId)
    {
        var performanceImplementer = await _context.PerformanceImplementers.Where(performanceImplementer =>
            performanceImplementer.PerformanceId == performanceId &&
            performanceImplementer.ImplementerId == implementerId).FirstOrDefaultAsync();
        
        if (performanceImplementer == null)
        {
            return false;
        }
        _context.Remove(performanceImplementer);
        var implementer = await _context.Implementers.FindAsync(implementerId);
        if (implementer == null)
        {
            return false;
        }
        _context.Remove(implementer);
        await _context.SaveChangesAsync();
        return true;
    }
}