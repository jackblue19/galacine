using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Entities;

namespace Application.Interfaces.Repositories
{
    public interface IInventoryRepository : IGenericRepository<Inventory>
    {
        Task<Inventory?> GetByItemAsync(int itemId);
        // Giảm tồn kho khi bán/addon
        Task<bool> DecreaseStockAsync(int itemId, int quantity);
    }
}
