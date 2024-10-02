namespace Grilo.Api.Dtos
{
    public class RefreshTokenInputDTO
    {
        public required string RefreshToken { get; set; }
    }

    public class RefreshTokenOutputDTO
    {
        public required string AccessToken { get; set; }
        public required string RefreshToken { get; set; }
    }
}