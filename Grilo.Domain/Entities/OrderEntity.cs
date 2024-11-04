namespace Grilo.Domain.Entities
{
    public class OrderEntity(string orderNo, string accountId, decimal amount)
    {
        public string Id { get; private set; } = Guid.NewGuid().ToString();
        public string OrderNo { get; set; } = orderNo;
        public string AccountId { get; set; } = accountId;
        public AccountEntity Account { get; set; } = null!;
        public decimal Amount { get; set; } = amount;
        public IList<OrderItemEntity> Items { get; private set; } = [];
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public void AddItem(OrderItemEntity input)
        {
            Items.Add(input);
        }
        public void RaiseAmount(decimal amount)
        {
            Amount += amount;
        }
    }
}