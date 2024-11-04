namespace Grilo.Domain.Dtos.Order
{
    public class CreateOrderItemDTO
    {
        public required string ItemId { get; set; }
        public int Quantity { get; set; }
    }
}