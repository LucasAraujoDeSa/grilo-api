using Grilo.Domain.Enums;

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

        public void AddItem(OrderItemEntity input)
        {
            Items.Add(input);
        }
        public void RaiseAmount(decimal amount)
        {
            Amount += amount;
        }
        public void SetInProgress()
        {
            Status = OrderStatusEnum.IN_PROGRESS.ToString();
            IsActive = true;
        }

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
    }
}