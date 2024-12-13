using Bogus;
using Grilo.Domain.Dtos;

namespace Grilo.Test.Mocks.Item
{
    public class CreateItemInputMock
    {
        public static CreateItemDTO GenerateMock(){
            var input = new Faker<CreateItemDTO>()
                .RuleFor(property => property.Title, faker => faker.Commerce.ProductName())
                .RuleFor(property => property.Quantity, faker => faker.Random.Int(10, 20))
                .RuleFor(property => property.Price, faker => Convert.ToDecimal(faker.Commerce.Price()));

            return input;
        }
    }
}