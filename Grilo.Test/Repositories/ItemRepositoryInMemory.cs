using Grilo.Aplication.Repositories;
using Grilo.Domain.Entities;

namespace Grilo.Test.Repositories
{
    public class ItemRepositoryInMemory : IItemRepository
    {
        private IList<ItemEntity> _data = [];
        public async Task<bool> CheckTitle(string title)
        {
            return await Task.FromResult(_data.FirstOrDefault(item => item.Title == title) is not null);
        }

        public async Task<bool> Delete(string id)
        {
            ItemEntity? item = _data.FirstOrDefault(item => item.Id == id);
            if(item is not null){
                _data.Remove(item);
                return await Task.FromResult(true);
            }
            return await Task.FromResult(false);
        }

        public async Task<IEnumerable<ItemEntity>> Get()
        {
            return await Task.FromResult(_data);
        }

        public async Task<ItemEntity?> GetById(string id)
        {
            ItemEntity? item = _data.FirstOrDefault(item => item.Id == id);
            return await Task.FromResult(item);
        }

        public async Task Save(ItemEntity input)
        {
            _data.Add(input);
            await Task.CompletedTask;
        }

        public async Task Update(ItemEntity input)
        {
            ItemEntity? item = _data.FirstOrDefault(item => item.Id == input.Id);
            if(item is not null){
                item.Price = input.Price;
                item.Quantity = input.Quantity;
                item.Title = input.Title;
            }

            await Task.CompletedTask;
        }
    }
}