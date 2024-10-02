using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Grilo.Api.Database;
using Grilo.Api.Dtos;
using Grilo.Api.Entities;
using Grilo.Api.Helper;
using Grilo.Api.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Grilo.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController(DatabaseContext context, IConfiguration configuration) : ControllerBase
    {
        private DatabaseContext _context { get; set; } = context;
        private IConfiguration _configuration { get; set; } = configuration;
        private string _generateJWT(string id, DateTime expireIn)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var secret = _configuration["Keys:Auth"];
            var key = Encoding.ASCII.GetBytes(secret is not null ? secret : string.Empty);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity([
                    new Claim(type: "id", value: id)
                ]),
                Expires = expireIn,
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256
                )
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private string? _decode(string token)
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

        [HttpPost("Singup")]
        public async Task<ActionResult<Result<SignupOutputDTO>>> Signup([FromBody] SignupInputDTO input)
        {
            try
            {
                bool emailIsInUse = _context.Account.FirstOrDefault(item => item.Email == input.Email) != null;

                if (emailIsInUse)
                {
                    return Result<SignupOutputDTO>.OperationalError("Email is already in use");
                }

                if (input.Password != input.PasswordConfirmation)
                {
                    return Result<SignupOutputDTO>.OperationalError("Passwords do not match");
                }

                AccountEntity newAccount = new(
                    email: input.Email,
                    name: input.Name,
                    password: BCrypt.Net.BCrypt.HashPassword(input.Password));

                _context.Account.Add(newAccount);
                await _context.SaveChangesAsync();

                string access_token = _generateJWT(newAccount.Id, DateTime.UtcNow.AddHours(8));
                string refreshToken = _generateJWT(newAccount.Id, DateTime.UtcNow.AddDays(15));

                Result<SignupOutputDTO> result = Result<SignupOutputDTO>.Created(
                    new SignupOutputDTO
                    {
                        AccessToken = access_token,
                        Email = newAccount.Email,
                        Name = newAccount.Name,
                        RefreshToken = refreshToken
                    }, "Account created");

                return StatusCode(StatusCodeHelper.Get(result.Status), result);
            }
            catch (Exception exc)
            {
                return StatusCode(500, exc.Message);
            }
        }

        [HttpPost("Signin")]
        public async Task<ActionResult<Result<SigninOutputDTO>>> Signin([FromBody] SigninInputDTO input)
        {
            try
            {
                var account = await _context.Account.FirstOrDefaultAsync(item => item.Email == input.Email);

                if (account is null)
                {
                    return Result<SigninOutputDTO>.OperationalError("Email or password incorrect");
                }

                var passwordIsValid = BCrypt.Net.BCrypt.Verify(input.Password, account.Password);

                if (!passwordIsValid)
                {
                    return Result<SigninOutputDTO>.OperationalError("Email or password incorrect");
                }

                string access_token = _generateJWT(account.Id, DateTime.UtcNow.AddHours(8));
                string refreshToken = _generateJWT(account.Id, DateTime.UtcNow.AddDays(15));

                Result<SigninOutputDTO> result = Result<SigninOutputDTO>.Ok(
                    new SigninOutputDTO
                    {
                        Id = account.Id,
                        Name = account.Name,
                        AccessToken = access_token,
                        RefreshToken = refreshToken
                    }, "Singin success!!");

                return StatusCode(StatusCodeHelper.Get(result.Status), result);
            }
            catch (Exception exc)
            {
                return StatusCode(500, exc.Message);
            }
        }

        [HttpPost("Signout")]
        public ActionResult<Result<bool>> Signout()
        {
            try
            {
                Result<bool> result = Result<bool>.Ok(
                    true, "Singout success!!");

                return StatusCode(StatusCodeHelper.Get(result.Status), result);
            }
            catch (Exception exc)
            {
                return StatusCode(500, exc.Message);
            }
        }

        [HttpPost("RefreshToken")]
        public ActionResult<Result<RefreshTokenOutputDTO>> RefreshToken([FromBody] RefreshTokenInputDTO input)
        {
            try
            {
                var id = _decode(input.RefreshToken);

                if (id is null)
                {
                    return StatusCode(
                        400,
                        Result<RefreshTokenOutputDTO>.OperationalError("Invalid refresh token")
                    );
                }

                string access_token = _generateJWT(id, DateTime.UtcNow.AddHours(8));
                string refreshToken = _generateJWT(id, DateTime.UtcNow.AddDays(15));


                Result<RefreshTokenOutputDTO> result = Result<RefreshTokenOutputDTO>.Ok(
                    new()
                    {
                        AccessToken = access_token,
                        RefreshToken = refreshToken
                    }, "RefreshToken success!!");

                return StatusCode(StatusCodeHelper.Get(result.Status), result);
            }
            catch (Exception exc)
            {
                return StatusCode(500, exc.Message);
            }
        }
    }
}