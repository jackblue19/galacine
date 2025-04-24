using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Application.DTOs;
namespace Application.Interfaces.Services;

public interface IVoucherService
{
    Task<List<VoucherDto>> GetAllAsync();
    Task<VoucherDto?> GetByIdAsync(int id);
    Task CreateAsync(VoucherDto dto);
    Task UpdateAsync(VoucherDto dto);
    Task DeleteAsync(int id);
}

