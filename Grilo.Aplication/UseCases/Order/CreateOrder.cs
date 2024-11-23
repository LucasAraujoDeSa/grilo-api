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

        private async Task AddItemToOrder(
            CreateOrderItemDTO orderItem,
            OrderEntity order
        )
        {
        
            ItemEntity? currentItem = await _itemRepository.GetById(orderItem.ItemId);
            if (currentItem is not null)
            {
                order.RaiseAmount(currentItem.Price * orderItem.Quantity);
                if (currentItem.Quantity <= orderItem.Quantity)
                {
                    // to be implemented
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

                IList<Task> orderItemsTasks = [];

                foreach(CreateOrderItemDTO orderItem in input.OrderItems){
                    orderItemsTasks.Add(AddItemToOrder(orderItem, newOrder));
                }

                await Task.WhenAll(orderItemsTasks);

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