using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UL.Calculator.Core.Model;
using UL.Calculator.Services;

namespace UL.Calculator.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Authenticate Credentials
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /account/authenticate
        ///     {
        ///        "username": "user",
        ///        "password": "Your passsword"
        ///     }
        /// </remarks>
        /// <param name="credentials"></param>
        /// <returns>Userinfo containing Token</returns>
        /// <response code="200">Returns Userinfo with Token</response>
        /// <response code="401">For Invalid Credentials</response>
        [AllowAnonymous]
        [HttpPost("authenticate")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Authenticate([FromBody] Credentials credentials)
        {
            var user = await _userService.Authenticate(credentials);

            if (user == null)
                return Unauthorized(new { message = "Username or password is incorrect" });
            return Ok(user);
        }
    }
}