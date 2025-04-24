using Application.Interfaces.Repositories;
using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class ItemRepository : GenericRepository<Item>, IItemRepository
    {
        private readonly CinemaDbContext _context;

        public ItemRepository(CinemaDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Item>> GetByCategoryAsync(string itemCategory)
        {
            return await _context.Items
                .Where(i => i.ItemCategory == itemCategory)
                .ToListAsync();
        }

        public async Task<IEnumerable<Item>> GetByTypeAsync(string type)
        {
            return await _context.Items
                .Where(i => i.Type == type)
                .ToListAsync();
        }
    }
}
