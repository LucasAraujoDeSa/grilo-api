using Grilo.Application.Adapters;
using Grilo.Application.Repositories;
using Grilo.Domain.Dtos;
using Grilo.Domain.Entities;
using Grilo.Shared.Utils;

namespace Grilo.Application.UseCases.Account
{
    public class SignIn(IAccountRepository accountRepository, IEncrypter encrypter) : IUseCase<SigninInputDTO, SigninOutputDTO?>
    {
        private readonly IAccountRepository _accountRepository = accountRepository;
        private readonly IEncrypter _encrypter = encrypter;

        public async Task<Result<SigninOutputDTO?>> Execute(SigninInputDTO input)
        {
            try
            {
                AccountEntity? account = await _accountRepository.GetByEmail(input.Email);

                if (account is null)
                {
                    return Result<SigninOutputDTO?>.OperationalError("Email or password incorrect");
                }

                bool passwordIsValid = _encrypter.Compare(input.Password, account.Password);

                if (!passwordIsValid)
                {
                    return Result<SigninOutputDTO?>.OperationalError("Email or password incorrect");
                }

                string access_token = _encrypter.GenerateAccessToken(account.Id);
                string refreshToken = _encrypter.GenerateRefreshToken(account.Id);

                return Result<SigninOutputDTO?>.Ok(
                    new()
                    {
                        Id = account.Id,
                        Name = account.Name,
                        AccessToken = access_token,
                        RefreshToken = refreshToken
                    },
                    "Singin success!!"
                );
            }
            catch (Exception exc)
            {
                return Result<SigninOutputDTO?>.InternalError(exc.Message);
            }
        }
    }
}