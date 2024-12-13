using Grilo.Application.Repositories;
using Grilo.Domain.Entities;
using Grilo.Domain.Enums;
using Grilo.Shared.Utils;

namespace Grilo.Application.UseCases.Order
{
    public class MarkAsDone(IOrderRepository repository) : IUseCase<string, bool>
    {
        private readonly IOrderRepository _repository = repository;

        public async Task<Result<bool>> Execute(string id)
        {
            try
            {
                OrderEntity? order = await _repository.GetById(id);

                if (order is null)
                {
                    return Result<bool>.NotFound("Order not exist");
                }

                string inProgressStatus = OrderStatusEnum.IN_PROGRESS.ToString();
                if (order.Status != inProgressStatus)
                    return Result<bool>.OperationalError("Cannot change order with status different from IN PROGRESS");

                order.SetAsDone();

                foreach (OrderItemEntity orderItem in order.Items)
                {
                    int quantity = orderItem.Quantity;
                    orderItem.Item.DecreaseQuantity(quantity);
                }

                await _repository.Update(order);

                return Result<bool>.Ok(true, "Success!");
            }
            catch (Exception exc)
            {
                return Result<bool>.InternalError(exc.Message);
            }
        }
    }
}