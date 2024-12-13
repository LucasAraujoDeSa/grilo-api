using Grilo.Domain.Entities;

namespace Grilo.Application.Repositories
{
    public interface IItemRepository
    {
        Task<bool> CheckTitle(string title);
        Task<ItemEntity?> GetById(string id);
        Task<IList<ItemEntity>> GetItems(IList<string> itemsId);
        Task Save(ItemEntity input);
        Task Update(ItemEntity input);
        Task<IEnumerable<ItemEntity>> Get();
        Task<bool> Delete(string id);
    }
}