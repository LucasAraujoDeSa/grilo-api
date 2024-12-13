using Grilo.Application.Repositories;
using Grilo.Domain.Entities;
using Grilo.Shared.Utils;

namespace Grilo.Application.UseCases.Item
{
    public class GetAllItems(IItemRepository repository) : IUseCase<IEnumerable<ItemEntity>>
    {
        private readonly IItemRepository _repository = repository;
        public async Task<Result<IEnumerable<ItemEntity>?>> Execute()
        {
            try
            {
                IEnumerable<ItemEntity> result = await _repository.Get();

                return Result<IEnumerable<ItemEntity>?>.Ok(result, "Success!");
            }
            catch (Exception exc)
            {
                return Result<IEnumerable<ItemEntity>?>.InternalError(exc.Message);
            }
        }
    }
}