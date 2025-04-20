using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces;
using Data;
using Application.Interfaces.Repositories;
using Data.Entities;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class RoleRepository : GenericRepository<Role>, IRoleRepository
    {

        public RoleRepository(CinemaDbContext context) : base(context)
        {
        }

        public async Task<Role?> GetByDescAsync(string desc)
            => string.IsNullOrWhiteSpace(desc)
                ? null
                : await _dbSet.FirstOrDefaultAsync(r => r.RoleDesc.ToLower() == desc.ToLower());
    }

}
