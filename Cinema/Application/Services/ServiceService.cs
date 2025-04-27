using Application.DTOs;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Data.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Services;

public class ServiceService : IServiceService
{
    private readonly IServiceRepository _serviceRepository;

    public ServiceService(IServiceRepository serviceRepository)
    {
        _serviceRepository = serviceRepository;
    }

    public async Task<List<ServiceDto>> GetAllAsync()
    {
        var services = await _serviceRepository.GetAllAsync();
        return services.Select(s => new ServiceDto
        {
            ServiceId = s.ServiceId,
            ServiceDesc = s.ServiceDesc,
            CreatedBy = s.CreatedBy,
            ApprovedBy = s.ApprovedBy,
            IsApproved = s.IsApproved,
            Note = s.Note,
            CreatedAt = s.CreatedAt,
            UpdatedAt = s.UpdatedAt
        }).ToList();
    }

    public async Task<ServiceDto?> GetByIdAsync(int id)
    {
        var s = await _serviceRepository.GetByIdAsync(id);
        if (s == null) return null;

        return new ServiceDto
        {
            ServiceId = s.ServiceId,
            ServiceDesc = s.ServiceDesc,
            CreatedBy = s.CreatedBy,
            ApprovedBy = s.ApprovedBy,
            IsApproved = s.IsApproved,
            Note = s.Note,
            CreatedAt = s.CreatedAt,
            UpdatedAt = s.UpdatedAt
        };
    }

    public async Task CreateAsync(CreateServiceDto dto)
    {
        var service = new Service
        {
            ServiceDesc = dto.ServiceDesc,
            CreatedBy = dto.CreatedBy,
            Note = dto.Note,
            CreatedAt = DateTime.UtcNow,
            IsApproved = false
        };
        await _serviceRepository.AddAsync(service);
    }

    public async Task UpdateAsync(int id, UpdateServiceDto dto)
    {
        var service = await _serviceRepository.GetByIdAsync(id);
        if (service == null) throw new Exception("Service not found");

        service.ServiceDesc = dto.ServiceDesc;
        service.Note = dto.Note;
        service.UpdatedAt = DateTime.UtcNow;

        await _serviceRepository.UpdateAsync(service);
    }

    public async Task DeleteAsync(int id)
    {
        var service = await _serviceRepository.GetByIdAsync(id);
        if (service == null) throw new Exception("Service not found");

        await _serviceRepository.DeleteAsync(service.ServiceId);

    }

    public async Task ApproveAsync(int serviceId, int approvedByUserId)
    {
        var service = await _serviceRepository.GetByIdAsync(serviceId);
        if (service == null) throw new Exception("Service not found");

        service.IsApproved = true;
        service.ApprovedBy = approvedByUserId;
        service.UpdatedAt = DateTime.UtcNow;

        await _serviceRepository.UpdateAsync(service);
    }

    public async Task<List<ServiceDto>> GetPendingAsync()
    {
        var pending = await _serviceRepository.GetPendingApprovalAsync();
        return pending.Select(s => new ServiceDto
        {
            ServiceId = s.ServiceId,
            ServiceDesc = s.ServiceDesc,
            CreatedBy = s.CreatedBy,
            ApprovedBy = s.ApprovedBy,
            IsApproved = s.IsApproved,
            Note = s.Note,
            CreatedAt = s.CreatedAt,
            UpdatedAt = s.UpdatedAt
        }).ToList();
    }
}
