using ChatAPI.Exceptions;
using ChatAPI.Models.Dtos;
using ChatAPI.Models.Enumerables;
using ChatAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ChatAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly ITokenService _tokenService;
        private readonly IAccountService _accountService;

        public AccountController(
            ITokenService tokenService,
            IAccountService accountService)
        {
            _tokenService = tokenService;
            _accountService = accountService;
        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<string>> LoginAsync(LoginDto loginDto)
        {
            try
            {
                var user = await _accountService.LoginAsync(loginDto);

                if (user == null)
                {
                    return BadRequest();
                }

                return Ok(_tokenService.CreateToken(user));
            }
            catch(ChatApiException ex)
            {
                return StatusCode(500, ((ErrorCodes)ex.ErrorCode).ToString());
            }
        }
    }
}
