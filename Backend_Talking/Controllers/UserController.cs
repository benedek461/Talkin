using AutoMapper;
using Backend_Talking.Models.Dtos;
using ChatAPI.Data;
using ChatAPI.Exceptions;
using ChatAPI.Models;
using ChatAPI.Models.Dtos;
using ChatAPI.Models.Enumerables;
using ChatAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ChatAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly ChatDbContext _chatDbContext;

        public UserController(
            IUserService userService,
            IMapper mapper,
            ChatDbContext chatDbContext)
        {
            _userService = userService;
            _mapper = mapper;
            _chatDbContext = chatDbContext;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<ActionResult<CreateUserDto>> RegisterUserAsync(CreateUserDto createUserDto)
        {
            try
            {
                /*
                if (await _userService.ValidateEmailAsync(createUserDto.Email))
                {
                    var user = await _userService.RegisterUserAsync(createUserDto);

                    return Created("", _mapper.Map<CreateUserDto>(user));
                }
                */

                if (await _userService.ValidateUsernameAsync(createUserDto.userName))
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

        [HttpGet]
        [Route("GetUserByUsername/{userName}")]
        [Authorize]
        public async Task<IActionResult> GetUserByUsername([FromRoute] string userName)
        {
            var user = await _userService.GetUserByUsernameAsync(userName);
            
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpGet]
        [Route("GetUserById/{Id}")]
        [Authorize]
        public async Task<ActionResult<UserDto>> GetUserById([FromRoute] int Id)
        {
            var user = await _userService.GetByIdAsync(Id);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpGet]
        [Route("Me")]
        [Authorize]
        public async Task<ActionResult<UserDto>> GetUserAsync()
        {
            try
            {
                return await _userService.GetUserAsync();
            }
            catch (ChatApiException ex)
            {
                return StatusCode(500, ((ErrorCodes)ex.ErrorCode).ToString());
            }
        }

        [HttpPatch]
        [Route("Update")]
        [Authorize]
        public async Task<ActionResult<CreateUserDto>> UpdateUserAsync(UpdateUserDto updateUserDto)
        {
            try
            {
                return _mapper.Map<CreateUserDto>(await _userService.UpdateUserAsync(updateUserDto));
            }
            catch(ChatApiException ex)
            {
                return StatusCode(500, ((ErrorCodes)ex.ErrorCode).ToString());
            }
        }
    }
}
