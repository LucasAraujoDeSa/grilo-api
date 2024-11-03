using Grilo.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Grilo.Infra.Database
{
    public class DatabaseContext(DbContextOptions options) : DbContext(options)
    {
        public DbSet<ItemEntity> Item { get; set; }
        public DbSet<AccountEntity> Account { get; set; }
        public DbSet<TokenBlackListEntity> TokenBlackList { get; set; }
    }
}