using Grilo.Aplication.Repositories;
using Grilo.Aplication.UseCases.Item;
using Grilo.Infra.Repositories;

namespace Grilo.Api.Dependencies
{
    public class Item
    {
        public static void Initialize(WebApplicationBuilder builder)
        {
            builder.Services.AddTransient<IItemRepository, ItemRepository>();
            builder.Services.AddTransient<CreateItem>();
            builder.Services.AddTransient<DeleteItem>();
            builder.Services.AddTransient<UpdateItem>();
            builder.Services.AddTransient<GetAllItems>();
            builder.Services.AddTransient<GetItemById>();
        }
    }
}