using Grilo.Aplication.Repositories;
using Grilo.Domain.Entities;
using Grilo.Shared.Utils;

namespace Grilo.Aplication.UseCases.Item
{
    public class DeleteItem(IItemRepository repository) : IUseCase<string, bool>
    {
        private readonly IItemRepository _repository = repository;
        public async Task<Result<bool>> Execute(string id)
        {
            try
            {
                ItemEntity? item = await _repository.GetById(id);

                if (item is null)
                {
                    return Result<bool>.NotFound("Item not exist!");
                }

                await _repository.Delete(id);

                return Result<bool>.Ok(true, "Success!");
            }
            catch (Exception exc)
            {
                return Result<bool>.InternalError(exc.Message);
            }
        }
    }
}