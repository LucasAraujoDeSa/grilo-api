using Grilo.Domain.Dtos.Order.CreateOrder;
using Grilo.Domain.Enums;
using Grilo.Shared.Utils;

namespace Grilo.Domain.Entities
{
    public class OrderEntity(string orderNo, string accountId, decimal amount)
    {
        public string Id { get; private set; } = Guid.NewGuid().ToString();
        public string OrderNo { get; set; } = orderNo;
        public string AccountId { get; set; } = accountId;
        public AccountEntity Account { get; set; } = null!;
        public decimal Amount { get; set; } = amount;
        public IList<OrderItemEntity> Items { get; set; } = [];
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string Status { get; set; } = OrderStatusEnum.IN_PROGRESS.ToString();
        public bool IsActive { get; set; } = true;

        public void RaiseAmount(decimal amount) { Amount += amount; }

        public void ResetAmount() { Amount = 0; }

        public void ResetItems() { Items = []; }

        public void SetAsDone()
        {
            Status = OrderStatusEnum.DONE.ToString();
            IsActive = false;
        }

        public void SetCancelOrder()
        {
            Status = OrderStatusEnum.CANCEL.ToString();
            IsActive = false;
        }

        public Result<bool> AddItemToOrder(
            AddOrderItemToOrder orderItem
        )
        {
            RaiseAmount(orderItem.ItemPrice * orderItem.OrderItemQuantity);
            if (
                orderItem.ItemQuantity == 0 ||
                orderItem.OrderItemQuantity > orderItem.ItemQuantity ||
                orderItem.OrderItemQuantity < 0)
            {
                return Result<bool>.Failure($"invalid quantity for {orderItem.ItemTitle}");
            }
            Items.Add(new(
                itemId: orderItem.ItemId,
                orderId: Id,
                quantity: orderItem.OrderItemQuantity
            )
            { });
            return Result<bool>.Success();
        }
    }
}