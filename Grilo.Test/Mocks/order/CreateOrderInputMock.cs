using Grilo.Domain.Dtos.Order.CreateOrder;

namespace Grilo.Test.Mocks.order
{
    public class CreateOrderInputMock
    {
        public static CreateOrderDTO GenerateMock(string accountId)
        {
            CreateOrderDTO input = new()
            {
                AccountId = accountId,
                OrderItems = []
            };

            return input;
        }
    }
}