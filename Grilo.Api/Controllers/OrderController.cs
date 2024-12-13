using Grilo.Api.Attributes;
using Grilo.Api.Helper;
using Grilo.Application.UseCases.Order;
using Grilo.Domain.Dtos.Order;
using Grilo.Domain.Dtos.Order.GetAllOrders;
using Grilo.Shared.Utils;
using Microsoft.AspNetCore.Mvc;

namespace Grilo.Api.Controllers
{
    [ApiController]
    [CustomAuthorizeAttribute]
    [Route("api/[controller]")]
    public class OrderController(CreateOrder createOrder, GetAllOrders getAllOrders, MarkAsDone markAsDone) : ControllerBase
    {
        private readonly CreateOrder _createOrder = createOrder;
        private readonly GetAllOrders _getAllOrders = getAllOrders;
        private readonly MarkAsDone _markAsDone = markAsDone;

        [HttpPost]
        public async Task<ActionResult<Result<bool>>> CreateOrder([FromBody] IList<OrderItemDTO> input)
        {
            try
            {
                if (HttpContext.Items["accountId"] is not string accountId)
                {
                    return StatusCode(401, Result<object>.Unauthorized());
                }
                Result<bool> result = await _createOrder.Execute(new()
                {
                    AccountId = accountId,
                    OrderItems = input.Select(item => new OrderItemDTO()
                    {
                        ItemId = item.ItemId,
                        Quantity = item.Quantity
                    }).ToList()
                });
                return StatusCode(StatusCodeHelper.Get(result.Status), result);
            }
            catch (Exception exc)
            {
                return StatusCode(500, Result<object>.InternalError(exc.Message));
            }
        }

        [HttpGet]
        public async Task<ActionResult<Result<IEnumerable<GetAllOrdersOutputDTO>?>>> GetAll()
        {
            try
            {
                Result<IEnumerable<GetAllOrdersOutputDTO>?> result = await _getAllOrders.Execute();
                return StatusCode(StatusCodeHelper.Get(result.Status), result);
            }
            catch (Exception exc)
            {
                return StatusCode(500, Result<object>.InternalError(exc.Message));
            }
        }

        [HttpPatch("marskAsDone/{id}")]
        public async Task<ActionResult<Result<bool>>> MarkAsDone(string id)
        {
            try
            {
                Result<bool> result = await _markAsDone.Execute(id);
                return StatusCode(StatusCodeHelper.Get(result.Status), result);
            }
            catch (Exception exc)
            {
                return StatusCode(500, Result<object>.InternalError(exc.Message));
            }
        }
    }
}