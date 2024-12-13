using Grilo.Application.Repositories;
using Grilo.Domain.Dtos.Order.GetAllOrders;
using Grilo.Shared.Utils;

namespace Grilo.Application.UseCases.Order
{
    public class GetAllOrders(IOrderRepository repository) : IUseCase<IEnumerable<GetAllOrdersOutputDTO>?>
    {
        private readonly IOrderRepository _repository = repository;

        public async Task<Result<IEnumerable<GetAllOrdersOutputDTO>?>> Execute()
        {
            try
            {
                IEnumerable<GetAllOrdersOutputDTO> result = await _repository.GetAll();
                return Result<IEnumerable<GetAllOrdersOutputDTO>?>.Ok(result, "Success!");
            }
            catch (Exception exc)
            {
                return Result<IEnumerable<GetAllOrdersOutputDTO>?>.InternalError(exc.Message);
            }
        }
    }
}