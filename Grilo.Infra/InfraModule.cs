using Grilo.Application.Adapters;
using Grilo.Application.Repositories;
using Grilo.Infra.Adapters;
using Grilo.Infra.Database;
using Grilo.Infra.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Grilo.Infra
{
    public static class InfraModule
    {
        public static void AddInfra(this IServiceCollection services, IConfiguration configuration)
        {
            AddDatabase(services, configuration);
            AddRepositories(services);
            AddAdapters(services);
        }

        public static void AddDatabase(IServiceCollection services, IConfiguration configuration)
        {
            string connectionString = configuration["ApiSettings:Database:ConnectionString"] ?? throw new Exception("Connection string required");
            services.AddDbContext<DatabaseContext>(options => options.UseNpgsql(connectionString));
        }

        public static void AddRepositories(IServiceCollection services)
        {
            services.AddTransient<IOrderRepository, OrderRepository>();
            services.AddTransient<IItemRepository, ItemRepository>();
            services.AddTransient<IAccountRepository, AccountRepository>();
        }

        public static void AddAdapters(IServiceCollection services)
        {
            services.AddTransient<IEncrypter, Encrypter>();
        }
    }
}