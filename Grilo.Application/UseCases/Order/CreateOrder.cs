using Grilo.Application.Mappers;
using Grilo.Application.Repositories;
using Grilo.Domain.Dtos.Order.CreateOrder;
using Grilo.Domain.Entities;
using Grilo.Shared.Utils;

namespace Grilo.Application.UseCases.Order
{
    public class CreateOrder(
        IOrderRepository repository,
        IItemRepository itemRepository,
        IAccountRepository accountRepository
    ) : IUseCase<CreateOrderDTO, bool>
    {
        private readonly IOrderRepository _repository = repository;
        private readonly IItemRepository _itemRepository = itemRepository;
        private readonly IAccountRepository _accountRepository = accountRepository;

        public async Task<Result<bool>> Execute(CreateOrderDTO input)
        {
            try
            {
                if (input.OrderItems.Count == 0)
                {
                    return Result<bool>.NoContent(true, "no item informed");
                }

                bool accountExist = await _accountRepository.CheckById(input.AccountId);

                if (!accountExist)
                {
                    return Result<bool>.OperationalError("Account not exist");
                }

                OrderEntity newOrder = new(
                    orderNo: Guid.NewGuid().ToString(),
                    accountId: input.AccountId,
                    amount: 0
                )
                { };

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
                    Result<bool> addItemResult = newOrder.AddItemToOrder(orderItem);
                    if (
                        !addItemResult.IsSuccess
                        &&
                        addItemResult.Message is not null
                    )
                    {
                        return Result<bool>.OperationalError(addItemResult.Message);
                    }
                }

                await _repository.Save(newOrder);

                return Result<bool>.Created(true, "Order Created!");
            }
            catch (Exception exc)
            {
                return Result<bool>.InternalError(exc.Message);
            }
        }
    }
}