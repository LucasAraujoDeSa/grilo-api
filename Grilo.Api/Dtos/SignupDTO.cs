namespace Grilo.Api.Dtos
{
    public class SignupInputDTO
    {
        public required string Email { get; set; }
        public required string Name { get; set; }
        public required string Password { get; set; }
        public required string PasswordConfirmation { get; set; }
    }

    public class SignupOutputDTO
    {
        public required string Email { get; set; }
        public required string Name { get; set; }
        public required string AccessToken { get; set; }
        public required string RefreshToken { get; set; }
    }
}