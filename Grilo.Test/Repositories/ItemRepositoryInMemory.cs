using Grilo.Aplication.Repositories;
using Grilo.Domain.Entities;
using Grilo.Test.Mocks.Item;

namespace Grilo.Test.Repositories
{
    public class ItemRepositoryInMemory : IItemRepository
    {
        private IList<ItemEntity> _data = [];
        public void Populate()
        {
            for (int index = 1; index <= 10; index++)
            {
                var mock = ItemEntityMock.GenerateMock(index);
                _data.Add(mock);
            }
        }
        public async Task<bool> CheckTitle(string title)
        {
            return await Task.FromResult(_data.FirstOrDefault(item => item.Title == title) is not null);
        }

        public async Task<bool> Delete(string id)
        {
            ItemEntity? item = _data.FirstOrDefault(item => item.Id == id);
            if (item is not null)
            {
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
            if (item is not null)
            {
                item.Price = input.Price;
                item.Quantity = input.Quantity;
                item.Title = input.Title;
            }

            await Task.CompletedTask;
        }

        public async Task<IList<ItemEntity>> GetItems(IList<string> itemsId)
        {
            IList<ItemEntity> items = _data.Where(
                item => itemsId.Contains(item.Id)
            ).ToList();
            return await Task.FromResult(items);
        }
    }
}