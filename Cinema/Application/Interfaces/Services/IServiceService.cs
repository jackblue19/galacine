using Application.DTOs;

namespace Application.Interfaces.Services;

public interface IServiceService
{
    Task<List<ServiceDto>> GetAllAsync();
    Task<ServiceDto?> GetByIdAsync(int id);
    Task CreateAsync(CreateServiceDto dto);
    Task UpdateAsync(int id, UpdateServiceDto dto);
    Task DeleteAsync(int id);

    Task ApproveAsync(int serviceId, int approvedByUserId);
    Task<List<ServiceDto>> GetPendingAsync();
}
