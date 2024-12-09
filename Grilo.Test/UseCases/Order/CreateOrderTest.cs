using Grilo.Aplication.UseCases.Order;
using Grilo.Domain.Dtos.Order;
using Grilo.Domain.Entities;
using Grilo.Test.Mocks.Account;
using Grilo.Test.Mocks.order;
using Grilo.Test.Repositories;

namespace Grilo.Test.UseCases.Order
{
    public class CreateOrderTest
    {
        public required OrderRepositoryInMemory _orderRepository { get; set; }
        public required ItemRepositoryInMemory _itemRepository { get; set; }
        public required AccountRepositoryInMemory _accountRepository { get; set; }
        public CreateOrder MakeSut()
        {
            _orderRepository = new OrderRepositoryInMemory();
            _itemRepository = new ItemRepositoryInMemory();
            _accountRepository = new AccountRepositoryInMemory();
            CreateOrder sut = new(
                _orderRepository,
                _itemRepository,
                _accountRepository
            );

            return sut;
        }

        [Fact]
        public async Task TestCheckIfItemsWasInputed()
        {
            CreateOrder sut = MakeSut();
            AccountEntity account = CreateAccountMock.GenerateMock();
            await _accountRepository.Save(account);
            CreateOrderDTO input = CreateOrderInputMock.GenerateMock(account.Id);

            var result = await sut.Execute(input);

            Assert.Equal("NO_CONTENT", result.Status);
            Assert.True(result.IsSuccess);
        }

        [Fact]
        public async Task TestItemsQuantityError()
        {
            CreateOrder sut = MakeSut();
            AccountEntity account = CreateAccountMock.GenerateMock();
            await _accountRepository.Save(account);
            CreateOrderDTO input = CreateOrderInputMock.GenerateMock(account.Id);
            _itemRepository.Populate();
            var items = await _itemRepository.Get();

            input.OrderItems = items.Select(item => new CreateOrderItemDTO()
            {
                ItemId = item.Id,
                Quantity = item.Quantity + 10
            }).ToList();

            var result = await sut.Execute(input);

            Assert.Equal("OPERATIONAL_ERROR", result.Status);
            Assert.False(result.IsSuccess);
        }

        [Fact]
        public async Task TestItemsNotExist()
        {
            CreateOrder sut = MakeSut();
            AccountEntity account = CreateAccountMock.GenerateMock();
            await _accountRepository.Save(account);
            CreateOrderDTO input = CreateOrderInputMock.GenerateMock(account.Id);

            input.OrderItems = [
                new()
                {
                    ItemId = "invalidId",
                    Quantity = 10
                }
            ];

            var result = await sut.Execute(input);

            Assert.Equal("NOT_FOUND", result.Status);
            Assert.False(result.IsSuccess);
        }

        [Fact]
        public async Task TestCreateOrder()
        {
            CreateOrder sut = MakeSut();
            AccountEntity account = CreateAccountMock.GenerateMock();
            await _accountRepository.Save(account);
            CreateOrderDTO input = CreateOrderInputMock.GenerateMock(account.Id);
            _itemRepository.Populate();
            var items = await _itemRepository.Get();

            input.OrderItems = items.Select(item => new CreateOrderItemDTO()
            {
                ItemId = item.Id,
                Quantity = item.Quantity - 1
            }).ToList();

            var result = await sut.Execute(input);

            Assert.Equal("CREATED", result.Status);
            Assert.True(result.IsSuccess);
        }
    }
}