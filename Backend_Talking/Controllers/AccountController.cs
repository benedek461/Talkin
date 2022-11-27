using ChatAPI.Exceptions;
using ChatAPI.Models.Dtos;
using ChatAPI.Models.Enumerables;
using ChatAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChatAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly ITokenService _tokenService;
        private readonly IAccountService _accountService;
        private readonly IUserService _userService;

        public AccountController(
            ITokenService tokenService,
            IAccountService accountService,
            IUserService userService)
        {
            _tokenService = tokenService;
            _accountService = accountService;
            _userService = userService;
        }

        [HttpPost]
        [Route("login")]
        [Produces("text/plain")]
        public async Task<ActionResult<string>> LoginAsync(LoginDto loginDto)
        {
            try
            {
                var user = await _accountService.LoginAsync(loginDto);

                if (user == null)
                {
                    return BadRequest("Wrong username or password!");
                }

                return Ok(_tokenService.CreateToken(user));
            }
            catch(ChatApiException ex)
            {
                return StatusCode(500, ((ErrorCodes)ex.ErrorCode).ToString());
            }
        }

        [HttpDelete]
        [Route("logout")]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            var user = await _userService.GetUserAsync();

            if (user == null)
            {
                return Unauthorized();
            }

            return Ok();
        }
    }
}
