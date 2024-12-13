using Grilo.Domain.Entities;

namespace Grilo.Aplication.Repositories
{
    public interface IAccountRepository
    {
        Task<AccountEntity?> GetByEmail(string email);
        Task<AccountEntity?> GetById(string id);
        Task<bool> CheckEmail(string email);
        Task<bool> CheckById(string id);
        Task Save(AccountEntity input);
        Task AddToBlackList(TokenBlackListEntity input);
        Task<bool> CheckTokenInBlackList(string token);
    }
}