using Application.Interfaces.Repositories;
using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Application.DTOs;
using Application.Interfaces.Services;

namespace Application.Services;
public class VoucherService : IVoucherService
{
    private readonly IVoucherRepository _repo;

    public VoucherService(IVoucherRepository repo)
    {
        _repo = repo;
    }

    public async Task<List<VoucherDto>> GetAllAsync() =>
        (await _repo.GetAllAsync()).Select(v => new VoucherDto
        {
            VoucherId = v.VoucherId,
            Code = v.Code,
            VoucherDesc = v.VoucherDesc,
            DiscountPercent = v.DiscountPercent,
            ExpiredDate = v.ExpiredDate,
            IsActive = v.IsActive ?? false, // or `true`, depending on your logic

            MinPurchaseAmount = v.MinPurchaseAmount
        }).ToList();

    public async Task<VoucherDto> GetByIdAsync(int id)
    {
        var v = await _repo.GetByIdAsync(id);
        if (v == null) return null;

        return new VoucherDto
        {
            VoucherId = v.VoucherId,
            Code = v.Code,
            VoucherDesc = v.VoucherDesc,
            DiscountPercent = v.DiscountPercent,
            ExpiredDate = v.ExpiredDate,
            IsActive = v.IsActive ?? false, // or `true`, depending on your logic

            MinPurchaseAmount = v.MinPurchaseAmount
        };
    }

    public async Task CreateAsync(VoucherDto dto)
    {
        var entity = new Voucher
        {
            Code = dto.Code,
            VoucherDesc = dto.VoucherDesc,
            DiscountPercent = dto.DiscountPercent,
            ExpiredDate = dto.ExpiredDate,
            IsActive = dto.IsActive,
            MinPurchaseAmount = dto.MinPurchaseAmount
        };
        await _repo.AddAsync(entity);
    }

    public async Task UpdateAsync(VoucherDto dto)
    {
        var entity = await _repo.GetByIdAsync(dto.VoucherId);
        if (entity != null)
        {
            entity.Code = dto.Code;
            entity.VoucherDesc = dto.VoucherDesc;
            entity.DiscountPercent = dto.DiscountPercent;
            entity.ExpiredDate = dto.ExpiredDate;
            entity.IsActive = dto.IsActive;
            entity.MinPurchaseAmount = dto.MinPurchaseAmount;

            await _repo.UpdateAsync(entity);
        }
    }

    public async Task DeleteAsync(int id) => await _repo.DeleteAsync(id);
}

