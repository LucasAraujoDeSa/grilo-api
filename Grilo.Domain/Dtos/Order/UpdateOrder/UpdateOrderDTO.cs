namespace Grilo.Domain.Dtos.Order.UpdateOrder
{
    public class UpdateOrderDTO
    {
        public required string OrderId { get; set; }
        public IList<OrderItemDTO> OrderItems { get; set; } = [];
    }
}