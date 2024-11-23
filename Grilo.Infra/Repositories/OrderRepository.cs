using Grilo.Aplication.Repositories;
using Grilo.Domain.Dtos.Order.GetAllOrders;
using Grilo.Domain.Entities;
using Grilo.Infra.Database;
using Microsoft.EntityFrameworkCore;

namespace Grilo.Infra.Repositories
{
    public class OrderRepository(DatabaseContext context) : IOrderRepository
    {
        private readonly DatabaseContext _context = context;

        public async Task<IEnumerable<GetAllOrdersOutputDTO>> GetAll()
        {
            IEnumerable<OrderEntity> result = await _context
                .Order
                .Include(item => item.Account)
                .Include(item => item.Items)
                .ThenInclude(row => row.Item)
                .ToListAsync();

            return result.Select(item => new GetAllOrdersOutputDTO()
            {
                Id = item.Id,
                OrderNo = item.OrderNo,
                Amount = item.Amount,
                CreatedAt = item.CreatedAt,
                Status = item.Status,
                Items = item.Items.Select(row => new GetAllOrdersItemDTO()
                {
                    Id = row.Item.Id,
                    Title = row.Item.Title,
                    Price = row.Item.Price,
                    Quantity = row.Quantity
                }).ToList()
            });
        }

        public async Task<OrderEntity?> GetById(string id)
        {
            OrderEntity? result = await _context.Order
                .Include(row => row.Items)
                .ThenInclude(item => item.Item)
                .FirstOrDefaultAsync(item => item.Id == id);
            return result;
        }

        public async Task Save(OrderEntity input)
        {
            _context.Order.Add(input);
            await _context.SaveChangesAsync();
        }

        public async Task Update(OrderEntity input)
        {
            _context.Order.Update(input);
            await _context.SaveChangesAsync();
        }
    }
}