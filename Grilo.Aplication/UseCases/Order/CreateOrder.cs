using Grilo.Aplication.Repositories;
using Grilo.Domain.Dtos.Order;
using Grilo.Domain.Entities;
using Grilo.Shared.Utils;

namespace Grilo.Aplication.UseCases.Order
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

        private async Task AddItemToOrder(
            CreateOrderItemDTO orderItem,
            OrderEntity order,
            IList<string> hasErrorOnQuantity
        )
        {

            ItemEntity? currentItem = await _itemRepository.GetById(orderItem.ItemId);
            if (currentItem is not null)
            {
                order.RaiseAmount(currentItem.Price * orderItem.Quantity);
                if (
                    currentItem.Quantity == 0 ||
                    orderItem.Quantity > currentItem.Quantity ||
                    orderItem.Quantity < 0)
                {
                    hasErrorOnQuantity.Add($"invalid quantity for {currentItem.Title}");
                }
                order.Items.Add(new(
                    itemId: currentItem.Id,
                    orderId: order.Id,
                    quantity: orderItem.Quantity
                )
                { });
            }
        }
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

                IList<string> ErrorsList = [];

                await Task.WhenAll(
                    input.OrderItems.Select(item =>
                        AddItemToOrder(item, newOrder, ErrorsList)
                    ).ToList()
                );

                if (ErrorsList.Any())
                {
                    return Result<bool>.OperationalError(ErrorsList.First());
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