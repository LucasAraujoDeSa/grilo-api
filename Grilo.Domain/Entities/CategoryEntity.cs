using System.Text.Json.Serialization;

namespace Grilo.Domain.Entities
{
    public class CategoryEntity(string title)
    {
        public string Id { get; private set; } = Guid.NewGuid().ToString();
        public string Title { get; set; } = title;
        public DateTime CreatedAt { get; set; } = DateTime.Now.ToUniversalTime();
        public bool IsActive { get; set; } = true;
        [JsonIgnore]
        public IList<ItemEntity> Items { get; set; } = [];
    }
}