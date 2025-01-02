namespace Grilo.Domain.Dtos.Category
{
    public class CreateCategoryInputDTO
    {
        public required string Title { get; set; }
    }

    public class CreateCategoryOutputDTO
    {
        public required string Id { get; set; }
        public required string Title { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}