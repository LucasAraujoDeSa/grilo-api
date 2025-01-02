namespace Grilo.Domain.Dtos.Category
{
    public class GetAllCategoriesDTO
    {
        public required string Id { get; set; }
        public required string Title { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}