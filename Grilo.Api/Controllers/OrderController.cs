using Grilo.Api.Attributes;
using Grilo.Api.Helper;
using Grilo.Aplication.UseCases.Order;
using Grilo.Domain.Dtos.Order;
using Grilo.Shared.Utils;
using Microsoft.AspNetCore.Mvc;

namespace Grilo.Api.Controllers
{
    [ApiController]
    [CustomAuthorizeAttribute]
    [Route("api/[controller]")]
    public class OrderController(CreateOrder createOrder) : ControllerBase
    {
        private readonly CreateOrder _createOrder = createOrder;

        [HttpPost]
        public async Task<ActionResult<Result<bool>>> CreateOrder([FromBody] IList<RequestCreateOrderDTO> input)
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
                    OrderItems = input.Select(item => new CreateOrderItemDTO()
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
    }
}