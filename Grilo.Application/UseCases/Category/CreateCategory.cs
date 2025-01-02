using Grilo.Application.Mappers;
using Grilo.Application.Repositories;
using Grilo.Domain.Dtos.Category;
using Grilo.Domain.Entities;
using Grilo.Shared.Utils;

namespace Grilo.Application.UseCases.Category
{
    public class CreateCategory(ICategoryRepository categoryRepository) : IUseCase<CreateCategoryInputDTO, CreateCategoryOutputDTO>
    {
        private readonly ICategoryRepository _categoryRepository = categoryRepository;
        public async Task<Result<CreateCategoryOutputDTO?>> Execute(CreateCategoryInputDTO input)
        {
            try
            {
                bool titleIsInUse = await _categoryRepository.CheckCategoryByTitle(input.Title);

                if (titleIsInUse)
                {
                    return Result<CreateCategoryOutputDTO?>.OperationalError("Title already in use");
                }

                CategoryEntity newCategory = new(
                    title: input.Title
                );

                await _categoryRepository.Save(newCategory);

                CreateCategoryOutputDTO result = CategoryEntityToCreateCategoryOutputDTO.Make(newCategory);

                return Result<CreateCategoryOutputDTO?>.Created(result, "Created!");
            }
            catch (Exception exc)
            {
                return Result<CreateCategoryOutputDTO?>.InternalError(exc.Message);
            }
        }
    }
}