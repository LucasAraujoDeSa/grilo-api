using Grilo.Application.Repositories;
using Grilo.Domain.Dtos;
using Grilo.Domain.Entities;
using Grilo.Shared.Utils;

namespace Grilo.Application.UseCases.Item
{
    public class CreateItem(IItemRepository repository) : IUseCase<CreateItemDTO, ItemEntity>
    {
        private readonly IItemRepository _repository = repository;

        public async Task<Result<ItemEntity?>> Execute(CreateItemDTO input)
        {
            try
            {
                bool titleIsInUse = await _repository.CheckTitle(input.Title);

                if (titleIsInUse)
                {
                    return Result<ItemEntity?>.OperationalError("Title already in use!");
                }

                ItemEntity newItem = new(
                    title: input.Title,
                    price: input.Price,
                    quantity: input.Quantity,
                    categoryId: input.CategoryId
                );

                await _repository.Save(newItem);

                return Result<ItemEntity?>.Created(newItem, "Item created successfully!!!");
            }
            catch (Exception exc)
            {
                return Result<ItemEntity?>.InternalError(exc.Message);
            }
        }
    }
}