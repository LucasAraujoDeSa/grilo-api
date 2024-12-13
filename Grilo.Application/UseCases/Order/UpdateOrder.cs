using Grilo.Application.Mappers;
using Grilo.Application.Repositories;
using Grilo.Domain.Dtos.Order.CreateOrder;
using Grilo.Domain.Dtos.Order.UpdateOrder;
using Grilo.Domain.Entities;
using Grilo.Shared.Utils;

namespace Grilo.Application.UseCases.Order
{
    public class UpdateOrder(
        IOrderRepository repository,
        IItemRepository itemRepository
    ) : IUseCase<UpdateOrderDTO, bool>
    {
        private readonly IOrderRepository _repository = repository;
        private readonly IItemRepository _itemRepository = itemRepository;

        public async Task<Result<bool>> Execute(UpdateOrderDTO input)
        {
            try
            {
                OrderEntity? order = await _repository.GetById(input.OrderId);

                if (order is null)
                {
                    return Result<bool>.NotFound("Order not found!");
                }

                order.ResetAmount();
                order.ResetItems();

                IList<ItemEntity> items = await _itemRepository.GetItems(
                    input.OrderItems.Select(item => item.ItemId).ToList()
                );

                if (items.Count < input.OrderItems.Count)
                {
                    return Result<bool>.OperationalError("One of the informed items does not exist");
                }

                IList<AddOrderItemToOrder> orderItems = ToAddOrderItemToOrderList.Make(
                    items,
                    input.OrderItems
                );

                foreach (var orderItem in orderItems)
                {
                    Result<bool> addItemResult = order.AddItemToOrder(orderItem);
                    if (
                        !addItemResult.IsSuccess
                        &&
                        addItemResult.Message is not null
                    )
                    {
                        return Result<bool>.OperationalError(addItemResult.Message);
                    }
                }

                await _repository.Update(order);

                return Result<bool>.Ok(true, "Order updated!");
            }
            catch (Exception exc)
            {
                return Result<bool>.InternalError(exc.Message);
            }
        }
    }
}