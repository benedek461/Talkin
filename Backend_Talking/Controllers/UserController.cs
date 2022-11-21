using AutoMapper;
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
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UserController(
            IUserService userService,
            IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<ActionResult<CreateUserDto>> RegisterUserAsync(CreateUserDto createUserDto)
        {
            try
            {
                if (await _userService.ValidateEmailAsync(createUserDto.Email))
                {
                    var user = await _userService.RegisterUserAsync(createUserDto);

                    return Created("", _mapper.Map<CreateUserDto>(user));
                }

                return BadRequest();
            }
            catch(ChatApiException ex)
            {
                return StatusCode(500, ((ErrorCodes)ex.ErrorCode).ToString());
            }
        }

        [HttpGet]
        [Route("AllUsers")]
        [Authorize]
        public async Task<ActionResult<List<UserDto>>> GetUsersAsync()
        {
            try
            {
                return await _userService.GetUsersAsync();
            }
            catch(ChatApiException ex)
            {
                return StatusCode(500, ((ErrorCodes)ex.ErrorCode).ToString());
            }
        }
    }
}
