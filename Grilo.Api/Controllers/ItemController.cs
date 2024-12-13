using Grilo.Api.Attributes;
using Grilo.Domain.Dtos;
using Grilo.Api.Helper;
using Grilo.Shared.Utils;
using Grilo.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Grilo.Aplication.UseCases.Item;

namespace Grilo.Api.Controllers
{
    [ApiController]
    [CustomAuthorizeAttribute]
    [Route("api/[controller]")]
    public class ItemController(
        CreateItem createItem,
        UpdateItem upadteItem,
        DeleteItem deleteItem,
        GetAllItems getAllItems,
        GetItemById getItem
    ) : ControllerBase
    {
        private readonly CreateItem _createItem = createItem;
        private readonly UpdateItem _upadteItem = upadteItem;
        private readonly DeleteItem _deleteItem = deleteItem;
        private readonly GetAllItems _getAllItems = getAllItems;
        private readonly GetItemById _getItem = getItem;

        #region CreateItem
        [HttpPost]
        public async Task<ActionResult<Result<ItemEntity?>>> Create([FromBody] CreateItemDTO input)
        {
            try
            {
                Result<ItemEntity?> result = await _createItem.Execute(input);
                return StatusCode(StatusCodeHelper.Get(result.Status), result);
            }
            catch (Exception exc)
            {
                return StatusCode(500, Result<object>.InternalError(exc.Message));
            }
        }
        #endregion

        #region GetAllItems
        [HttpGet]
        public async Task<ActionResult<Result<IEnumerable<ItemEntity>>>> GetAll()
        {
            try
            {
                Result<IEnumerable<ItemEntity>?> result = await _getAllItems.Execute();
                return StatusCode(StatusCodeHelper.Get(result.Status), result);
            }
            catch (Exception exc)
            {
                return StatusCode(500, Result<object>.InternalError(exc.Message));
            }
        }
        #endregion

        #region GetItemById
        [HttpGet("{id}")]
        public async Task<ActionResult<Result<ItemEntity?>>> GetById(string id)
        {
            try
            {
                Result<ItemEntity?> result = await _getItem.Execute(id);
                return StatusCode(StatusCodeHelper.Get(result.Status), result);
            }
            catch (Exception exc)
            {
                return StatusCode(500, Result<object>.InternalError(exc.Message));
            }
        }
        #endregion

        #region UpdateItem
        [HttpPut]
        public async Task<ActionResult<Result<ItemEntity>>> Update([FromBody] UpdateItemDTO input)
        {
            try
            {
                Result<ItemEntity?> result = await _upadteItem.Execute(input);
                return StatusCode(StatusCodeHelper.Get(result.Status), result);
            }
            catch (Exception exc)
            {
                return StatusCode(500, Result<object>.InternalError(exc.Message));
            }
        }
        #endregion

        #region DeleteRegion
        [HttpDelete("{id}")]
        public async Task<ActionResult<Result<bool>>> Delete(string id)
        {
            try
            {
                Result<bool> result = await _deleteItem.Execute(id);
                return StatusCode(StatusCodeHelper.Get(result.Status), result);
            }
            catch (Exception exc)
            {
                return StatusCode(500, Result<object>.InternalError(exc.Message));
            }
        }
    }
    #endregion
}