using Grilo.Domain.Entities;

namespace Grilo.Aplication.Repositories
{
    public interface IOrderRepository
    {
        Task Save(OrderEntity input);
    }
}