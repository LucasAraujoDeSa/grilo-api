using Grilo.Domain.Entities;

namespace Grilo.Test.Mocks.Account
{
    public class CreateAccountMock
    {
        public static AccountEntity GenerateMock()
        {
            return new(
                name: "user_test",
                email: "user_test@email.com",
                password: "pass@Test1234567"
            );
        }
    }
}