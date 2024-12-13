using Grilo.Application.Repositories;
using Grilo.Domain.Entities;
using Grilo.Infra.Database;
using Microsoft.EntityFrameworkCore;

namespace Grilo.Infra.Repositories
{
    public class AccountRepository(DatabaseContext context) : IAccountRepository
    {
        private readonly DatabaseContext _context = context;
        public async Task AddToBlackList(TokenBlackListEntity input)
        {
            _context.TokenBlackList.Add(input);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> CheckById(string id)
        {
            bool exist = await _context.Account.FirstOrDefaultAsync(item => item.Id == id) is not null;
            return exist;
        }

        public async Task<bool> CheckEmail(string email)
        {
            bool exist = await _context.Account.FirstOrDefaultAsync(item => item.Email == email) is not null;
            return exist;
        }

        public async Task<bool> CheckTokenInBlackList(string token)
        {
            bool exist = await _context.TokenBlackList.FirstOrDefaultAsync(item => item.Token == token) is not null;
            return exist;
        }

        public async Task<AccountEntity?> GetByEmail(string email)
        {
            AccountEntity? account = await _context.Account.FirstOrDefaultAsync(item => item.Email == email);
            return account;
        }

        public async Task<AccountEntity?> GetById(string id)
        {
            AccountEntity? account = await _context.Account.FirstOrDefaultAsync(item => item.Id == id);
            return account;
        }

        public async Task Save(AccountEntity input)
        {
            _context.Account.Add(input);
            await _context.SaveChangesAsync();
        }
    }
}