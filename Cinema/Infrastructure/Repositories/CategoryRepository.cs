using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces.Repositories;
using Data.Entities;
using Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(CinemaDbContext context) : base(context)
        {
        }
        public async Task<Category?> GetByDescriptionAsync(string desc)
            => string.IsNullOrWhiteSpace(desc)
                ? null
                : await _dbSet.FirstOrDefaultAsync(c => c.CategoryDesc.ToLower() == desc.ToLower());
    }
}
