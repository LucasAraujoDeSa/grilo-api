namespace Grilo.Domain.Entities
{
    public class TokenBlackListEntity(string token)
    {
        public string Id { get; private set; } = Guid.NewGuid().ToString();
        public string Token { get; set; } = token;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}