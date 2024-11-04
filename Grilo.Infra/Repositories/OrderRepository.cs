using Grilo.Aplication.Repositories;
using Grilo.Domain.Entities;
using Grilo.Infra.Database;

namespace Grilo.Infra.Repositories
{
    public class OrderRepository(DatabaseContext context) : IOrderRepository
    {
        private readonly DatabaseContext _context = context;

        public async Task Save(OrderEntity input)
        {
            _context.Order.Add(input);
            await _context.SaveChangesAsync();
        }
    }
}