using Application.Interfaces.Repositories;
using Data;
using Data.Entities;
using Infrastructure;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

public class ServiceRepository : GenericRepository<Service>, IServiceRepository
{
    private readonly CinemaDbContext _context;

    public ServiceRepository(CinemaDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Service>> GetPendingApprovalAsync()
    {
        return await _context.Services
            .Where(s => !s.IsApproved)
            .ToListAsync();
    }

    public async Task ApproveAsync(int serviceId)
    {
        var service = await _context.Services.FindAsync(serviceId);
        if (service != null)
        {
            service.IsApproved = true;
            service.UpdatedAt = DateTime.Now;
            await _context.SaveChangesAsync();
        }
    }
}
