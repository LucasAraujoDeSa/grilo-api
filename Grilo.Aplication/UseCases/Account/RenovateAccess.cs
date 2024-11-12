using Grilo.Aplication.Adapters;
using Grilo.Aplication.Repositories;
using Grilo.Domain.Dtos;
using Grilo.Domain.Entities;
using Grilo.Shared.Utils;

namespace Grilo.Aplication.UseCases.Account
{
    public class RenovateAccess(IAccountRepository accountRepository, IEncrypter encrypter) : IUseCase<RefreshTokenInputDTO, RefreshTokenOutputDTO?>
    {
        private readonly IAccountRepository _accountRepository = accountRepository;
        private readonly IEncrypter _encrypter = encrypter;

        public async Task<Result<RefreshTokenOutputDTO?>> Execute(RefreshTokenInputDTO input)
        {
            try
            {
                string? id = _encrypter.DecodeRefreshToken(input.RefreshToken);

                if (id is null)
                {
                    return Result<RefreshTokenOutputDTO?>.OperationalError("Invalid refresh token");
                }

                AccountEntity? account = await _accountRepository.GetById(id);

                if (account is null)
                {
                    return Result<RefreshTokenOutputDTO?>.OperationalError("Account not exist");
                }

                bool tokenIsInBlackList = await _accountRepository.CheckTokenInBlackList(input.RefreshToken);

                if (tokenIsInBlackList)
                {
                    return Result<RefreshTokenOutputDTO?>.OperationalError("Invalid refresh token");
                }

                string access_token = _encrypter.GenerateAccessToken(id);
                string refreshToken = _encrypter.GenerateRefreshToken(id);


                await _accountRepository.AddToBlackList(new(token: input.RefreshToken));

                return Result<RefreshTokenOutputDTO?>.Ok(
                    new()
                    {
                        AccessToken = access_token,
                        RefreshToken = refreshToken,
                        UserName = account.Name
                    },
                    "Access Renovate success!!"
                );
            }
            catch (Exception exc)
            {
                return Result<RefreshTokenOutputDTO?>.InternalError(exc.Message);
            }
        }
    }
}