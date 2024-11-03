using Grilo.Aplication.Repositories;
using Grilo.Domain.Dtos;
using Grilo.Domain.Entities;
using Grilo.Shared.Utils;

namespace Grilo.Aplication.UseCases.Item
{
    public class UpdateItem(IItemRepository repository) : IUseCase<UpdateItemDTO, ItemEntity>
    {
        private readonly IItemRepository _repository = repository;
        public async Task<Result<ItemEntity?>> Execute(UpdateItemDTO input)
        {
            try
            {
                ItemEntity? item = await _repository.GetById(input.Id);

                if (item == null)
                {
                    return Result<ItemEntity?>.NotFound("item not exist");
                }

                item.Title = input.Title;
                item.Price = input.Price;
                item.Quantity = input.Quantity;

                await _repository.Update(item);

                return Result<ItemEntity?>.Ok(item, "success!!!");
            }
            catch (Exception exc)
            {
                return Result<ItemEntity?>.InternalError(exc.Message);
            }
        }
    }
}