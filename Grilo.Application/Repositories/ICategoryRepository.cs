using Grilo.Domain.Dtos.Category;
using Grilo.Domain.Entities;

namespace Grilo.Application.Repositories
{
    public interface ICategoryRepository
    {
        Task<bool> CheckCategoryByTitle(string title);
        Task Save(CategoryEntity input);
        Task<IList<GetAllCategoriesDTO>> Get(string? title);
    }
}