using Data.Entities;

namespace Application.Interfaces.Repositories
{
    public interface IItemRepository : IGenericRepository<Item>
    {
        Task<IEnumerable<Item>> GetByTypeAsync(string type);
    }
}
