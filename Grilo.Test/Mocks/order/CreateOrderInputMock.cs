using Bogus;
using Grilo.Domain.Dtos.Order;

namespace Grilo.Test.Mocks.order
{
    public class CreateOrderInputMock
    {
        public static CreateOrderDTO GenerateMock(string accountId){
            CreateOrderDTO input = new (){
                AccountId=accountId,
                OrderItems=[]
            };

            return input;
        }
    }
}