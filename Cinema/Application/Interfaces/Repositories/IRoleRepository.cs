using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Entities;

namespace Application.Interfaces.Repositories
{
    public interface IRoleRepository : IGenericRepository<Role>
    {
        Task<User?> GetByUsnAsync(string? usn);
        Task<User?> GetByEmailAsync(string? email);
        Task<IEnumerable<User>> GetByRoleAsync(string role);
        Task<IEnumerable<User>> GetByNameAsync(string? name);
        Task<IEnumerable<User>> GetByPhoneAsync(string? phone);
        Task<Role?> GetByDescAsync(string desc);
    }
}
