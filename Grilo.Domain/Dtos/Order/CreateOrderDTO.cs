namespace Grilo.Domain.Dtos.Order
{
    public class CreateOrderDTO
    {
        public required string AccountId { get; set; }
        public IList<CreateOrderItemDTO> OrderItems { get; set; } = [];
    }
}