namespace Grilo.Api.Dtos
{
    public class CreateItemDTO
    {
        public required string Title { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}