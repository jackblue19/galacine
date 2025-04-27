using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Application.DTOs;

namespace Application.Interfaces.Services;

public interface IItemService
{
    Task<List<ItemDto>> GetAllAsync();
    Task<ItemDto?> GetByIdAsync(int id);
    Task CreateAsync(CreateItemDto dto);
    Task UpdateAsync(int id, UpdateItemDto dto);
    Task DeleteAsync(int id);
    Task<List<ItemDto>> GetByTypeAsync(string type); // For Food/Drink
}

