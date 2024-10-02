namespace Grilo.Api.Entities
{
    public class ItemEntity(string title, decimal price, int quantity)
    {
        public string Id { get; private set; } = Guid.NewGuid().ToString();
        public string Title { get; set; } = title;
        public decimal Price { get; set; } = price;
        public int Quantity { get; set; } = quantity;
    }
}