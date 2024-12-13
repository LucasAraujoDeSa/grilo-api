using Grilo.Application.Adapters;
using Grilo.Infra.Adapters;

namespace Grilo.Api.Dependencies
{
    public class Adapters
    {
        public static void Initialize(WebApplicationBuilder builder)
        {
            builder.Services.AddTransient<IEncrypter, Encrypter>();
        }
    }
}