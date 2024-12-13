using Grilo.Domain.Dtos.Order;
using Grilo.Domain.Dtos.Order.CreateOrder;
using Grilo.Domain.Entities;

namespace Grilo.Application.Mappers
{
    public class ToAddOrderItemToOrderList
    {
        public static IList<AddOrderItemToOrder> Make(
            IList<ItemEntity> items,
            IList<OrderItemDTO> orderItems
        )
        {
            IList<AddOrderItemToOrder> orderItemsList = items.Join(
                orderItems,
                item => item.Id,
                orderItem => orderItem.ItemId,
                (item, orderItem) => new AddOrderItemToOrder()
                {
                    ItemId = item.Id,
                    ItemTitle = item.Title,
                    ItemPrice = item.Price,
                    ItemQuantity = item.Quantity,
                    OrderItemQuantity = orderItem.Quantity
                }
            ).ToList();

            return orderItemsList;
        }
    }
}