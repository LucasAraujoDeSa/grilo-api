using Grilo.Application.Repositories;
using Grilo.Domain.Dtos.Category;
using Grilo.Shared.Utils;

namespace Grilo.Application.UseCases.Category
{
    public class GetAllCategories(ICategoryRepository categoryRepository) : IUseCase<string?, IList<GetAllCategoriesDTO>?>
    {
        private readonly ICategoryRepository _categoryRepository = categoryRepository;
        public async Task<Result<IList<GetAllCategoriesDTO>?>> Execute(string? title)
        {
            try
            {
                IList<GetAllCategoriesDTO> result = await _categoryRepository.Get(title);

                return Result<IList<GetAllCategoriesDTO>?>.Ok(result, "Success!");
            }
            catch (Exception exc)
            {
                return Result<IList<GetAllCategoriesDTO>?>.InternalError(exc.Message);
            }
        }
    }
}