using Grilo.Aplication.Repositories;
using Grilo.Domain.Entities;
using Grilo.Infra.Database;
using Microsoft.EntityFrameworkCore;

namespace Grilo.Infra.Repositories
{
    public class ItemRepository(DatabaseContext context) : IItemRepository
    {
        private readonly DatabaseContext _context = context;

        public async Task<bool> CheckTitle(string title)
        {
            bool exist = await _context.Item.FirstOrDefaultAsync(item => item.Title == title) is not null;
            return exist;
        }

        public async Task<bool> Delete(string id)
        {
            ItemEntity? item = await _context.Item.FirstOrDefaultAsync(item => item.Id == id);
            if (item is null) return false;
            _context.Item.Remove(item);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<ItemEntity>> Get()
        {
            IEnumerable<ItemEntity> result = await _context.Item.ToListAsync();
            return result;
        }

        public async Task<ItemEntity?> GetById(string id)
        {
            ItemEntity? result = await _context.Item.FirstOrDefaultAsync(item => item.Id == id);
            return result;
        }

        public async Task Save(ItemEntity input)
        {
            _context.Item.Add(input);
            await _context.SaveChangesAsync();
        }

        public async Task Update(ItemEntity input)
        {
            _context.Item.Update(input);
            await _context.SaveChangesAsync();
        }
    }
}