using Grilo.Application.Repositories;
using Grilo.Application.UseCases.Order;
using Grilo.Infra.Repositories;

namespace Grilo.Api.Dependencies
{
    public class Order
    {
        public static void Initialize(WebApplicationBuilder builder)
        {
            builder.Services.AddTransient<IOrderRepository, OrderRepository>();
            builder.Services.AddTransient<CreateOrder>();
            builder.Services.AddTransient<GetAllOrders>();
            builder.Services.AddTransient<MarkAsDone>();
        }
    }
}