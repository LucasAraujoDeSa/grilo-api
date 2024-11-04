using Grilo.Aplication.Repositories;
using Grilo.Domain.Dtos.Order;
using Grilo.Domain.Entities;
using Grilo.Shared.Utils;

namespace Grilo.Aplication.UseCases.Order
{
    public class CreateOrder(IOrderRepository repository, IItemRepository itemRepository, IAccountRepository accountRepository) : IUseCase<CreateOrderDTO, bool>
    {
        private readonly IOrderRepository _repository = repository;
        private readonly IItemRepository _itemRepository = itemRepository;
        private readonly IAccountRepository _accountRepository = accountRepository;

        private async Task<bool> AddItemsToOrder(
            IList<CreateOrderItemDTO> orderItems,
            OrderEntity order
        )
        {
            foreach (var item in orderItems)
            {
                ItemEntity? currentItem = await _itemRepository.GetById(item.ItemId);
                if (currentItem is not null)
                {
                    order.RaiseAmount(currentItem.Price * item.Quantity);
                    if (currentItem.Quantity <= item.Quantity)
                    {
                        return false;
                    }
                    order.Items.Add(new(
                        itemId: currentItem.Id,
                        orderId: order.Id,
                        quantity: item.Quantity
                    )
                    { });
                }
            }

            return true;
        }
        public async Task<Result<bool>> Execute(CreateOrderDTO input)
        {
            try
            {
                if (!input.OrderItems.Any())
                {
                    return Result<bool>.NoContent(true, "no item informed");
                }

                bool accountExist = await _accountRepository.CheckById(input.AccountId);

                OrderEntity newOrder = new(
                    orderNo: Guid.NewGuid().ToString(),
                    accountId: input.AccountId,
                    amount: 0
                )
                { };

                bool quantityIsCorrect = await AddItemsToOrder(input.OrderItems, newOrder);

                if (!quantityIsCorrect)
                {
                    return Result<bool>.OperationalError("Quantity informed is invalid!");
                }

                if (newOrder.Items.Count != input.OrderItems.Count)
                {
                    return Result<bool>.NotFound("One of the informed items does not exist");
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