using Grilo.Api.Attributes;
using Grilo.Api.Database;
using Grilo.Api.Dtos;
using Grilo.Api.Entities;
using Grilo.Api.Helper;
using Grilo.Api.Utils;
using Microsoft.AspNetCore.Mvc;

namespace Grilo.Api.Controllers
{
    [ApiController]
    [CustomAuthorizeAttribute()]
    [Route("api/[controller]")]
    public class ItemController(DatabaseContext context) : ControllerBase
    {
        private readonly DatabaseContext _context = context;

        [HttpPost]
        public ActionResult<Result<ItemEntity>> Create([FromBody] CreateItemDTO input)
        {
            try
            {
                var titleIsInUse = _context.Item.FirstOrDefault(item => item.Title == input.Title);

                if (titleIsInUse != null)
                {
                    return Result<ItemEntity>.OperationalError("Title already exists");
                }

                ItemEntity newItem = new(
                    title: input.Title,
                    price: input.Price,
                    quantity: input.Quantity
                );
                _context.Item.Add(newItem);
                _context.SaveChanges();
                Result<ItemEntity> result = Result<ItemEntity>.Created(newItem, "Item created successfully!!!");
                return StatusCode(StatusCodeHelper.Get(result.Status), result);
            }
            catch (Exception exc)
            {
                return StatusCode(500, exc.Message);
            }
        }

        [HttpGet]
        public ActionResult<Result<IEnumerable<ItemEntity>>> GetAll()
        {
            try
            {
                Result<IEnumerable<ItemEntity>> result = Result<IEnumerable<ItemEntity>>.Ok([.. _context.Item], "success!!!");
                return StatusCode(StatusCodeHelper.Get(result.Status), result);
            }
            catch (Exception exc)
            {
                return StatusCode(500, exc.Message);
            }
        }

        [HttpGet("{id}")]
        public ActionResult<Result<ItemEntity?>> GetById(string id)
        {
            try
            {
                ItemEntity? item = _context.Item.Find(id);
                if (item == null)
                {
                    return NotFound("item not exist");
                }

                Result<ItemEntity?> result = Result<ItemEntity?>.Ok(item, "success!!!");
                return StatusCode(StatusCodeHelper.Get(result.Status), result);
            }
            catch (Exception exc)
            {
                return StatusCode(500, exc.Message);
            }
        }

        [HttpPut]
        public ActionResult<Result<ItemEntity>> Update([FromBody] UpdateItemDTO input)
        {
            try
            {
                ItemEntity? item = _context.Item.Find(input.Id);
                if (item == null)
                {
                    return NotFound("item not exist");
                }

                item.Title = input.Title;
                item.Price = input.Price;
                item.Quantity = input.Quantity;

                _context.SaveChanges();

                Result<ItemEntity> result = Result<ItemEntity>.Ok(item, "success!!!");
                return StatusCode(StatusCodeHelper.Get(result.Status), result);
            }
            catch (Exception exc)
            {
                return StatusCode(500, exc.Message);
            }
        }

        [HttpDelete("{id}")]
        public ActionResult<Result<bool>> Delete(string id)
        {
            try
            {
                ItemEntity? item = _context.Item.Find(id);
                if (item == null)
                {
                    return Result<bool>.NotFound("Item not found!!!");
                }

                _context.Item.Remove(item);
                _context.SaveChanges();

                Result<bool> result = Result<bool>.Ok(true, "Success!!!");
                return StatusCode(StatusCodeHelper.Get(result.Status), result);
            }
            catch (Exception exc)
            {
                return StatusCode(500, exc.Message);
            }
        }
    }
}