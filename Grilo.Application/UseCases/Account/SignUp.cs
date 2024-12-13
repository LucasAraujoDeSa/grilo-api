using Grilo.Application.Adapters;
using Grilo.Application.Repositories;
using Grilo.Domain.Dtos;
using Grilo.Domain.Entities;
using Grilo.Shared.Utils;

namespace Grilo.Application.UseCases.Account
{
    public class SignUp(IAccountRepository accountRepository, IEncrypter encrypter) : IUseCase<SignupInputDTO, SignupOutputDTO?>
    {
        private readonly IAccountRepository _accountRepository = accountRepository;
        private readonly IEncrypter _encrypter = encrypter;
        public async Task<Result<SignupOutputDTO?>> Execute(SignupInputDTO input)
        {
            try
            {
                bool emailIsInUse = await _accountRepository.CheckEmail(input.Email);

                if (emailIsInUse)
                {
                    return Result<SignupOutputDTO?>.OperationalError("Email is already in use");
                }

                if (input.Password != input.PasswordConfirmation)
                {
                    return Result<SignupOutputDTO?>.OperationalError("Passwords do not match");
                }

                string hashPassword = _encrypter.Hash(input.Password);

                AccountEntity newAccount = new(
                    email: input.Email,
                    name: input.Name,
                    password: hashPassword
                );

                await _accountRepository.Save(newAccount);

                string access_token = _encrypter.GenerateAccessToken(newAccount.Id);
                string refreshToken = _encrypter.GenerateRefreshToken(newAccount.Id);

                return Result<SignupOutputDTO?>.Created(
                    new()
                    {
                        AccessToken = access_token,
                        Email = newAccount.Email,
                        Name = newAccount.Name,
                        RefreshToken = refreshToken
                    },
                    "Account created"
                );
            }
            catch (Exception exc)
            {
                return Result<SignupOutputDTO?>.InternalError(exc.Message);
            }
        }
    }
}