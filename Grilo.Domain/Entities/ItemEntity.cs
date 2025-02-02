namespace Grilo.Domain.Entities
{
    public class ItemEntity(string title, decimal price, int quantity, string categoryId)
    {
        public string Id { get; private set; } = Guid.NewGuid().ToString();
        public string Title { get; set; } = title;
        public decimal Price { get; set; } = price;
        public int Quantity { get; set; } = quantity;
        public string? CategoryId { get; set; } = categoryId;
        public CategoryEntity? Category { get; set; } = null!;
        public void DecreaseQuantity(int quantity)
        {
            Quantity -= quantity;
        }
    }
}