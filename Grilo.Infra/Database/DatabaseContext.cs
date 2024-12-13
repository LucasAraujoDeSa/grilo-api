using Grilo.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Grilo.Infra.Database
{
    public class DatabaseContext(DbContextOptions options) : DbContext(options)
    {
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<OrderItemEntity>().HasKey(ic => new
            {
                ic.ItemId,
                ic.OrderId
            });
        }
        public required DbSet<ItemEntity> Item { get; set; }
        public required DbSet<AccountEntity> Account { get; set; }
        public required DbSet<TokenBlackListEntity> TokenBlackList { get; set; }
        public required DbSet<OrderEntity> Order { get; set; }
        public required DbSet<OrderItemEntity> OrderItem { get; set; }
    }
}