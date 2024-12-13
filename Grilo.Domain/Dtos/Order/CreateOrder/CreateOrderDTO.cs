namespace Grilo.Domain.Dtos.Order.CreateOrder
{
    public class CreateOrderDTO
    {
        public required string AccountId { get; set; }
        public IList<CreateOrderItemDTO> OrderItems { get; set; } = [];
    }
}