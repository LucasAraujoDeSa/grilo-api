using Grilo.Domain.Entities;

namespace Grilo.Test.Mocks.Item
{
    public class ItemEntityMock
    {
        public static ItemEntity GenerateMock(int index)
        {
            ItemEntity input = new(
                title: $"Item{index}",
                price: 12,
                quantity: 100
            );

            return input;
        }
    }
}