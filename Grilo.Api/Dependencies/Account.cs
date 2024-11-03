using Grilo.Aplication.Repositories;
using Grilo.Aplication.UseCases.Account;
using Grilo.Infra.Repositories;

namespace Grilo.Api.Dependencies
{
    public class Account
    {
        public static void Initialize(WebApplicationBuilder builder)
        {
            builder.Services.AddTransient<IAccountRepository, AccountRepository>();
            builder.Services.AddTransient<SignIn>();
            builder.Services.AddTransient<SignUp>();
            builder.Services.AddTransient<RenovateAccess>();
        }
    }
}