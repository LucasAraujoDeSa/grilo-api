namespace Grilo.Api.Entities
{
    public class AccountEntity(string email, string name, string password)
    {
        public string Id { get; private set; } = Guid.NewGuid().ToString();
        public string Email { get; set; } = email;
        public string Name { get; set; } = name;
        public string Password { get; set; } = password;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}