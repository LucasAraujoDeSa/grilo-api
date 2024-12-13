using Grilo.Application.Repositories;
using Grilo.Domain.Entities;

namespace Grilo.Test.Repositories
{
    public class AccountRepositoryInMemory : IAccountRepository
    {
        private IList<AccountEntity> _data = [];
        private IList<TokenBlackListEntity> _tokenBlackListData = [];
        public async Task AddToBlackList(TokenBlackListEntity input)
        {
            _tokenBlackListData.Add(input);
            await Task.CompletedTask;
        }

        public async Task<bool> CheckById(string id)
        {
            return await Task.FromResult(
                _data.FirstOrDefault(item => item.Id == id) is not null
            );
        }

        public async Task<bool> CheckEmail(string email)
        {
            return await Task.FromResult(
                _data.FirstOrDefault(item => item.Email == email) is not null
            );
        }

        public async Task<bool> CheckTokenInBlackList(string token)
        {
            return await Task.FromResult(
                _tokenBlackListData.FirstOrDefault(item => item.Token == token) is not null
            );
        }

        public async Task<AccountEntity?> GetByEmail(string email)
        {
            return await Task.FromResult(
                _data.FirstOrDefault(item => item.Email == email)
            );
        }

        public async Task<AccountEntity?> GetById(string id)
        {
            return await Task.FromResult(
                _data.FirstOrDefault(item => item.Id == id)
            );
        }

        public async Task Save(AccountEntity input)
        {
            _data.Add(input);
            await Task.CompletedTask;
        }
    }
}