namespace Grilo.Domain.Dtos.Order.CreateOrder
{
    public class AddOrderItemToOrder
    {
        public int OrderItemQuantity { get; set; }
        public required string ItemId { get; set; }
        public int ItemQuantity { get; set; }
        public decimal ItemPrice { get; set; }
        public required string ItemTitle { get; set; }
    }
}