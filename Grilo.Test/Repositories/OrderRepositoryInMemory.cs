using Grilo.Application.Repositories;
using Grilo.Domain.Dtos.Order.GetAllOrders;
using Grilo.Domain.Entities;

namespace Grilo.Test.Repositories
{
    public class OrderRepositoryInMemory : IOrderRepository
    {
        private IList<OrderEntity> _data = [];
        public async Task<IEnumerable<GetAllOrdersOutputDTO>> GetAll()
        {
            return await Task.FromResult(_data.Select(item => new GetAllOrdersOutputDTO()
            {
                Id = item.Id,
                OrderNo = item.OrderNo,
                Status = item.Status
            }).ToList());
        }

        public async Task<OrderEntity?> GetById(string id)
        {
            return await Task.FromResult(_data.FirstOrDefault(item => item.Id == id));
        }

        public async Task Save(OrderEntity input)
        {
            _data.Add(input);
            await Task.CompletedTask;
        }

        public async Task Update(OrderEntity input)
        {
            OrderEntity? order = _data.FirstOrDefault(item => item.Id == input.Id);
            if (order is not null)
            {
                order.Amount = input.Amount;
                order.Items = input.Items;
            }
            await Task.CompletedTask;
        }
    }
}