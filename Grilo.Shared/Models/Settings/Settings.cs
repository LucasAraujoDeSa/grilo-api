namespace Grilo.Shared.Models.Settings
{
    public class ApiSettings
    {
        public required DatabaseSettings Database { get; set; }
        public required JwtSettings Keys { get; set; }
    }
}
