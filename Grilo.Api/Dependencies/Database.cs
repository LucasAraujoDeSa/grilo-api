using Grilo.Infra.Database;
using Microsoft.EntityFrameworkCore;

namespace Grilo.Api.Dependencies
{
    public class Database
    {
        public static void Initialize(WebApplicationBuilder builder)
        {
            string connectionString = builder.Configuration["ApiSettings:Database:ConnectionString"] ?? throw new Exception("Connection string required");
            builder.Services.AddDbContext<DatabaseContext>(options => options.UseNpgsql(connectionString));
        }
    }
}