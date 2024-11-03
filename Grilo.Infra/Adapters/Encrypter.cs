using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Grilo.Aplication.Adapters;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Grilo.Infra.Adapters
{
    public class Encrypter(IConfiguration configuration) : IEncrypter
    {
        private IConfiguration _configuration { get; set; } = configuration;
        public bool Compare(string plaintext, string hash)
        {
            bool isCorrect = BCrypt.Net.BCrypt.Verify(plaintext, hash);
            return isCorrect;
        }

        public string? DecodeRefreshToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var secret = _configuration["Keys:Auth"];
            if (secret is null) return null;
            var key = Encoding.ASCII.GetBytes(secret);
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                ClockSkew = TimeSpan.Zero
            }, out SecurityToken validatedToken);


            var jwtToken = (JwtSecurityToken)validatedToken;
            var id = jwtToken.Claims.First(x => x.Type == "id").Value;

            return id;
        }

        public string GenerateAccessToken(string id)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var secret = _configuration["Keys:Auth"];
            var key = Encoding.ASCII.GetBytes(secret is not null ? secret : string.Empty);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity([
                    new Claim(type: "id", value: id)
                ]),
                Expires = DateTime.UtcNow.AddHours(8),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256
                )
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public string GenerateRefreshToken(string id)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var secret = _configuration["Keys:Auth"];
            var key = Encoding.ASCII.GetBytes(secret is not null ? secret : string.Empty);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity([
                    new Claim(type: "id", value: id)
                ]),
                Expires = DateTime.UtcNow.AddDays(15),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256
                )
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public string Hash(string plaintext)
        {
            string hash = BCrypt.Net.BCrypt.HashPassword(plaintext);
            return hash;
        }
    }
}