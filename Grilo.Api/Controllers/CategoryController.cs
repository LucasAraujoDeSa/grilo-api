using Grilo.Api.Helper;
using Grilo.Application.UseCases.Category;
using Grilo.Domain.Dtos.Category;
using Grilo.Shared.Utils;
using Microsoft.AspNetCore.Mvc;

namespace Grilo.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController(CreateCategory createCategory) : ControllerBase
    {
        private readonly CreateCategory _createCategory = createCategory;
        #region CreateCategory
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] CreateCategoryInputDTO input)
        {
            try
            {
                Result<CreateCategoryOutputDTO?> result = await _createCategory.Execute(input);
                return StatusCode(StatusCodeHelper.Get(result.Status), result);
            }
            catch (Exception exc)
            {
                return StatusCode(500, Result<object>.InternalError(exc.Message));
            }
        }
        #endregion
    }
}