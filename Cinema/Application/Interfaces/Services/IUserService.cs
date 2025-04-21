using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Entities;

namespace Application.Interfaces.Services
{
    public interface IUserService : IGenericService<User>
    {
        Task<User?> GetWithRoleAsync(int id);
    }
}
