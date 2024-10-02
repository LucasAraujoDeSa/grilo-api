using Grilo.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace Grilo.Api.Database
{
    public class DatabaseContext(DbContextOptions options) : DbContext(options)
    {
        public DbSet<ItemEntity> Item { get; set; }
        public DbSet<AccountEntity> Account { get; set; }
    }
}