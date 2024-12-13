using Grilo.Application.Repositories;
using Grilo.Application.UseCases.Item;
using Grilo.Domain.Dtos;
using Grilo.Test.Mocks.Item;
using Grilo.Test.Repositories;

namespace Grilo.Test.UseCases.Item
{
    public class CreateItemTest
    {
        public static CreateItem MakeSut()
        {
            IItemRepository itemRepo = new ItemRepositoryInMemory();
            CreateItem sut = new(itemRepo);
            return sut;
        }
        [Fact]
        public async Task TestCheckTitle()
        {
            CreateItem sut = MakeSut();
            CreateItemDTO input = CreateItemInputMock.GenerateMock();

            await sut.Execute(input);
            var result = await sut.Execute(input);

            Assert.False(result.IsSuccess);
            Assert.Equal("OPERATIONAL_ERROR", result.Status);
            Assert.Equal("Title already in use!", result.Message);
        }

        [Fact]
        public async Task TestCreateItem()
        {
            CreateItem sut = MakeSut();
            CreateItemDTO input = CreateItemInputMock.GenerateMock();

            var result = await sut.Execute(input);

            Assert.True(result.IsSuccess);
            Assert.Equal("CREATED", result.Status);
        }
    }
}