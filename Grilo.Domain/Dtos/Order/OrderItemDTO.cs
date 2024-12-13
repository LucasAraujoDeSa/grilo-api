namespace Grilo.Domain.Dtos.Order
{
    public class OrderItemDTO
    {
        public required string ItemId { get; set; }
        public int Quantity { get; set; }
    }
}