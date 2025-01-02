using Grilo.Domain.Dtos.Category;
using Grilo.Domain.Entities;

namespace Grilo.Application.Mappers
{
    public class CategoryEntityToCreateCategoryOutputDTO
    {
        public static CreateCategoryOutputDTO Make(CategoryEntity input)
        {
            return new()
            {
                Id = input.Id,
                Title = input.Title,
                CreatedAt = input.CreatedAt.ToUniversalTime()
            };
        }
    }
}