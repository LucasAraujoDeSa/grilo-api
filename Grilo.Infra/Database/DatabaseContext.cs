using Grilo.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Grilo.Infra.Database
{
    public class DatabaseContext(DbContextOptions options) : DbContext(options)
    {
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<OrderItemEntity>()
                .HasKey(item => new { item.ItemId, item.OrderId });
        }
        public DbSet<ItemEntity> Item { get; set; }
        public DbSet<AccountEntity> Account { get; set; }
        public DbSet<TokenBlackListEntity> TokenBlackList { get; set; }
        public DbSet<OrderEntity> Order { get; set; }
        public DbSet<OrderItemEntity> OrderItem { get; set; }
    }
}