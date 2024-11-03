namespace Grilo.Aplication.Adapters
{
    public interface IEncrypter
    {
        string Hash(string plaintext);
        bool Compare(string plaintext, string hash);
        string GenerateAccessToken(string id);
        string GenerateRefreshToken(string id);
        string? DecodeRefreshToken(string token);
    }
}