using Grilo.Domain.Entities;

namespace Grilo.Domain.Dtos.Order.GetAllOrders
{
    public class GetAllOrdersOutputDTO
    {
        public required string Id { get; set; }
        public required string OrderNo { get; set; }
        public decimal Amount { get; set; }
        public required string Status { get; set; }
        public IList<GetAllOrdersItemDTO> Items { get; set; } = [];
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}