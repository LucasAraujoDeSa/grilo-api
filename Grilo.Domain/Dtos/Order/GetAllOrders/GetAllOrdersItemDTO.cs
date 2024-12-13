namespace Grilo.Domain.Dtos.Order.GetAllOrders
{
    public class GetAllOrdersItemDTO
    {
        public required string Id { get; set; }
        public required string Title { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}