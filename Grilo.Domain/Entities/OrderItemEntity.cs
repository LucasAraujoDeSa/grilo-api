using System.ComponentModel.DataAnnotations.Schema;

namespace Grilo.Domain.Entities
{
    public class OrderItemEntity(string itemId, string orderId, int quantity)
    {
        public string ItemId { get; set; } = itemId;
        public ItemEntity Item { get; set; } = null!;
        public string OrderId { get; set; } = orderId;
        public OrderEntity Order { get; set; } = null!;
        public int Quantity { get; set; } = quantity;
    }
}