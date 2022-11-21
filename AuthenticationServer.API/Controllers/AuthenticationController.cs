using AuthenticationServer.API.Models.Data_Transfer_Objects;
using AuthenticationServer.API.Models.Domain_Objects;
using AuthenticationServer.API.Models.Responses;
using AuthenticationServer.API.Repositories.Interfaces;
using AuthenticationServer.API.Services;
using AuthenticationServer.API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AuthenticationServer.API.Controllers
{

    public class AuthenticationController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly AccessTokenGenerator _accessTokenGenerator;
        private readonly ITokenRepository _tokenRepository;

        public AuthenticationController(IUserRepository userRepository, IPasswordHasher passwordHasher, AccessTokenGenerator accessTokenGenerator, ITokenRepository tokenRepository)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _accessTokenGenerator = accessTokenGenerator;
            _tokenRepository = tokenRepository;
        }

        //================================REQUESTS================================//

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO registerDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequestModelState();
            }

            User existingUserByEmail = await _userRepository.GetByEmail(registerDTO.email);
            if (existingUserByEmail != null) 
            {
                return Conflict(new ErrorResponse("An account with this email is already existing..."));
            }

            User existingUserByUsername = await _userRepository.GetByUsername(registerDTO.userName);
            if (existingUserByUsername != null)
            {
                return Conflict(new ErrorResponse("An account with this username is already existing..."));
            }

            string passwordHash = _passwordHasher.HashPassword(registerDTO.password);
            User userToBeRegistered = new User()
            {
                userName = registerDTO.userName,
                PasswordHash = passwordHash,
                email = registerDTO.email,
                sex = registerDTO.sex,
                dateOfBirth = registerDTO.dateOfBirth,
                firstName = registerDTO.firstName,
                lastName = registerDTO.lastName
            };

            await _userRepository.Create(userToBeRegistered);

            return Ok();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequestModelState();
            }

            User userToBeLoggedIn = await _userRepository.GetByUsername(loginDTO.userName);
            if(userToBeLoggedIn == null)
            {
                return Unauthorized();
            }

            bool isPasswordCorrect = _passwordHasher.VerifyPassword(loginDTO.password, userToBeLoggedIn.PasswordHash);
            if (!isPasswordCorrect) 
            {
                return Unauthorized();
            }

            string accessToken = _accessTokenGenerator.GenerateToken(userToBeLoggedIn);

            //A token hozzáadaása adatbázishoz
            Tokens t = new Tokens()
            {
                userRefID = userToBeLoggedIn.user_ID,
                token = accessToken
            };
            await _tokenRepository.StoreToken(t);

            return Ok(new AuthenticatedResponse()
            {
                AccessToken = accessToken
            });
        }
        [Authorize]
        [HttpDelete("logout")]
        public async Task<IActionResult> Logout()
        {
            string rawUserId = HttpContext.User.FindFirstValue("id");
            if(!Guid.TryParse(rawUserId, out Guid userId))
            {
                return Unauthorized();
            }
            await _tokenRepository.DeleteToken(userId);
            return Ok();

        }

        [Authorize]
        [HttpGet("example")]
        public IActionResult Index()
        {
            return Ok();
        }

        [HttpGet("getUser")]
        [Route("{userName}")]
        public async Task<IActionResult> GetUserDataById([FromRoute] string userName)
        {
            User user = await _userRepository.GetByUsername(userName);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        //================================OTHER================================//
        IActionResult BadRequestModelState()
        {
            IEnumerable<string> errorMessage = ModelState.Values.SelectMany(x => x.Errors.Select(x => x.ErrorMessage));

            return BadRequest(new ErrorResponse(errorMessage));
        }
    }
}
