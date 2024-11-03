namespace Grilo.Domain.Dtos
{
    public class SigninInputDTO
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
    }

    public class SigninOutputDTO
    {
        public required string Id { get; set; }
        public required string Name { get; set; }
        public required string AccessToken { get; set; }
        public required string RefreshToken { get; set; }
    }
}