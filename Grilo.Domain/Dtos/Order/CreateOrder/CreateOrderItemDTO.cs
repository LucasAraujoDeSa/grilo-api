namespace Grilo.Domain.Dtos.Order.CreateOrder
{
    public class CreateOrderItemDTO
    {
        public required string ItemId { get; set; }
        public int Quantity { get; set; }
    }
}