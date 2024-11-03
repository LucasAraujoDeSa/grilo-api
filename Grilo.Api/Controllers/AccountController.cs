using Grilo.Domain.Dtos;
using Grilo.Api.Helper;
using Grilo.Shared.Utils;
using Microsoft.AspNetCore.Mvc;
using Grilo.Aplication.UseCases.Account;

namespace Grilo.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController(SignUp signup, SignIn signin, RenovateAccess renovateAccess) : ControllerBase
    {
        private readonly SignUp _signup = signup;
        private readonly SignIn _signin = signin;
        private readonly RenovateAccess _renovateAccess = renovateAccess;

        [HttpPost("Singup")]
        public async Task<ActionResult<Result<SignupOutputDTO>>> Signup([FromBody] SignupInputDTO input)
        {
            try
            {
                Result<SignupOutputDTO?> result = await _signup.Execute(input);
                return StatusCode(StatusCodeHelper.Get(result.Status), result);
            }
            catch (Exception exc)
            {
                return StatusCode(500, Result<object>.InternalError(exc.Message));
            }
        }

        [HttpPost("Signin")]
        public async Task<ActionResult<Result<SigninOutputDTO>>> Signin([FromBody] SigninInputDTO input)
        {
            try
            {
                Result<SigninOutputDTO?> result = await _signin.Execute(input);
                return StatusCode(StatusCodeHelper.Get(result.Status), result);
            }
            catch (Exception exc)
            {
                return StatusCode(500, Result<object>.InternalError(exc.Message));
            }
        }

        [HttpPost("RefreshToken")]
        public async Task<ActionResult<Result<RefreshTokenOutputDTO>>> RefreshToken([FromBody] RefreshTokenInputDTO input)
        {
            try
            {
                Result<RefreshTokenOutputDTO?> result = await _renovateAccess.Execute(input);
                return StatusCode(StatusCodeHelper.Get(result.Status), result);
            }
            catch (Exception exc)
            {
                return StatusCode(500, Result<object>.InternalError(exc.Message));
            }
        }
    }
}