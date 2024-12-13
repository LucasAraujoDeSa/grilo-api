using Grilo.Application.UseCases.Account;
using Grilo.Application.UseCases.Item;
using Grilo.Application.UseCases.Order;
using Microsoft.Extensions.DependencyInjection;

namespace Grilo.Application
{
    public static class ApplicationModule
    {
        public static void AddApplication(this IServiceCollection services)
        {
            AddUseCases(services);
        }

        public static void AddUseCases(IServiceCollection services)
        {
            services.AddTransient<SignIn>();
            services.AddTransient<SignUp>();
            services.AddTransient<RenovateAccess>();
            services.AddTransient<CreateItem>();
            services.AddTransient<DeleteItem>();
            services.AddTransient<UpdateItem>();
            services.AddTransient<GetAllItems>();
            services.AddTransient<GetItemById>();
            services.AddTransient<CreateOrder>();
            services.AddTransient<GetAllOrders>();
            services.AddTransient<MarkAsDone>();
            services.AddTransient<UpdateOrder>();
            services.AddTransient<CancelOrder>();
        }
    }
}