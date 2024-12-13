namespace Grilo.Domain.Dtos.Order.CreateOrder
{
    public class RequestCreateOrderDTO
    {
        public required string ItemId { get; set; }
        public int Quantity { get; set; }
    }
}