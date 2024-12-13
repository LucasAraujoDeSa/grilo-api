using Grilo.Application.Repositories;
using Grilo.Domain.Entities;
using Grilo.Shared.Utils;

namespace Grilo.Application.UseCases.Item
{
    public class GetItemById(IItemRepository repository) : IUseCase<string, ItemEntity?>
    {
        private readonly IItemRepository _repository = repository;
        public async Task<Result<ItemEntity?>> Execute(string id)
        {
            try
            {
                ItemEntity? item = await _repository.GetById(id);

                if (item is null)
                {
                    return Result<ItemEntity?>.NotFound("Item not exist!");
                }

                return Result<ItemEntity?>.Ok(item, "Success!");
            }
            catch (Exception exc)
            {
                return Result<ItemEntity?>.InternalError(exc.Message);
            }
        }
    }
}