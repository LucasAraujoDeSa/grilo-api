using Grilo.Domain.Dtos.Order.GetAllOrders;
using Grilo.Domain.Entities;

namespace Grilo.Aplication.Repositories
{
    public interface IOrderRepository
    {
        Task Save(OrderEntity input);
        Task<IEnumerable<GetAllOrdersOutputDTO>> GetAll();
        Task<OrderEntity?> GetById(string id);
        Task Update(OrderEntity input);
    }
}