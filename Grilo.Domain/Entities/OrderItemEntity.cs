using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Grilo.Domain.Entities
{
    public class OrderItemEntity(string itemId, string orderId, int quantity)
    {
        public string ItemId { get; set; } = itemId;
        [JsonIgnore]
        public ItemEntity Item { get; set; } = null!;
        public string OrderId { get; set; } = orderId;
        [JsonIgnore]
        public OrderEntity Order { get; set; } = null!;
        public int Quantity { get; set; } = quantity;
    }
}