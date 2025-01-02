using Grilo.Domain.Entities;

namespace Grilo.Test.Mocks.Item
{
    public class ItemEntityMock
    {
        public static ItemEntity GenerateMock(int index, string categoryId)
        {
            ItemEntity input = new(
                title: $"Item{index}",
                price: 12,
                quantity: 100,
                categoryId: categoryId
            );

            return input;
        }
    }
}