namespace Grilo.Api.Dtos
{
    public class UpdateItemDTO
    {
        public required string Id { get; set; }
        public required string Title { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}