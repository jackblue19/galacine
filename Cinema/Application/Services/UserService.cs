using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Data.Entities;

namespace Application.Services
{
    public class UserService : GenericService<User>, IUserService
    {
        private readonly IUserRepository _userRepo;
        public UserService(IGenericRepository<User> repo, IUserRepository userRepo) : base(repo)
        {
            _userRepo = userRepo;
        }

        public async Task<User?> GetByUserName(string userName)
        {
            return await _userRepo.GetByUsernameAsync(userName);
        }

        public async Task<User?> GetWithRoleAsync(int id)
            => await _userRepo.GetWithRoleAsync(id);
    }
}
