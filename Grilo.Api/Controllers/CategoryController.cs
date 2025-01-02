using Grilo.Api.Attributes;
using Grilo.Api.Helper;
using Grilo.Application.UseCases.Category;
using Grilo.Domain.Dtos.Category;
using Grilo.Shared.Utils;
using Microsoft.AspNetCore.Mvc;

namespace Grilo.Api.Controllers
{
    [ApiController]
    [CustomAuthorizeAttribute]
    [Route("api/[controller]")]
    public class CategoryController(
        CreateCategory createCategory,
        GetAllCategories getAllCategories
    ) : ControllerBase
    {
        private readonly CreateCategory _createCategory = createCategory;
        private readonly GetAllCategories _getAllCategories = getAllCategories;

        #region CreateCategory
        [HttpPost]
        public async Task<ActionResult<Result<CreateCategoryOutputDTO?>>> Create([FromBody] CreateCategoryInputDTO input)
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

        #region GetAllCategories
        [HttpGet]
        public async Task<ActionResult<IList<GetAllCategoriesDTO>>> Get([FromQuery] string? title)
        {
            try
            {
                Result<IList<GetAllCategoriesDTO>?> result = await _getAllCategories.Execute(title);
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