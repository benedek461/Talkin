using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.IdentityModel;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using TalkinWebAPI.Data;
using TalkinWebAPI.Models;
using TalkinWebAPI.Requests;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;

namespace TalkinWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        //A felhasználó
        public static User user = new User();
        private readonly IConfiguration _configuration;

        //Az adatbázishoz való hozzáférés
        private readonly TalkinAPIDbContext dbContext;

        public UserController(TalkinAPIDbContext dbContext, IConfiguration configuration)
        {
            this.dbContext = dbContext;
            _configuration = configuration;
        }

        //Felhasználók lekérése
        [HttpGet]
        [Route("GetUsers")]
        public async Task<IActionResult> GetUsers()
        {
            return Ok(await dbContext.User.ToListAsync());
        }

        //Egy darab felhasználó lekérése
        [HttpGet]
        [Route("GetSpecifiedUser/{user_ID:guid}")]
        public async Task<IActionResult> GetSpecifiedUser([FromRoute] Guid user_ID)
        {
            var user = await dbContext.User.FindAsync(user_ID);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        //Egy darab felhasználó lekérése felhasználónév alapján
        [HttpGet]
        [Route("GetSpecifiedUserByUsername/{userName}")]
        public async Task<IActionResult> GetSpecifiedUserByUsername([FromRoute] string userName)
        {
            var user = await dbContext.User.Where(x => x.userName == userName).SingleOrDefaultAsync();

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        //Egy felhasználó lekérése és a jelenlegivé tétele
        [HttpGet]
        [Route("GettingAndSettingCurrentUser")]
        public async Task<IActionResult> SetCurrentUser([FromRoute] string username)
        {
            var currentUser = await dbContext.User.Where(x => x.userName == username).SingleOrDefaultAsync();
            
            if (currentUser != null)
            {
                user.user_ID = currentUser.user_ID;
                user.userName = currentUser.userName;
                user.firstName = currentUser.firstName;
                user.lastName = currentUser.lastName;
                user.sex = currentUser.sex;
                user.email = currentUser.email;
                user.dateOfBirth = currentUser.dateOfBirth;
                user.PasswordHash = currentUser.PasswordHash;
                user.PasswordSalt = currentUser.PasswordSalt;

                return Ok();
            }

            return NotFound();
        }

        //Regisztráció
        [HttpPost]
        [Route("RegisterUser")]
        public async Task<IActionResult> RegisterUser(RegistrationDTO registrationRequest)
        {
            CreatePasswordHash(registrationRequest.password, out byte[] passwordHash, out byte[] passwordSalt);

            user.user_ID = new Guid();
            user.userName = registrationRequest.userName;
            user.firstName = registrationRequest.firstName;
            user.lastName = registrationRequest.lastName;
            user.sex = registrationRequest.sex;
            user.email = registrationRequest.email;
            user.dateOfBirth = registrationRequest.dateOfBirth;
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            await dbContext.User.AddAsync(user);
            await dbContext.SaveChangesAsync();

            return Ok(user);
        }

        //Bejelentkezés
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(LoginDTO request)
        {
            await SetCurrentUser(request.UserName);

            if (user.userName != request.UserName)
            {
                return BadRequest("User not Found!");
            }

            if (!VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
            {
                return BadRequest("Wrong password.");
            }

            string token = CreateToken(user);
            return Ok(token);
        }

        //Felhasználó törlése
        [HttpDelete]
        [Route("DeleteUser/{user_ID:guid}")]
        public async Task<IActionResult> DeleteUser([FromRoute] Guid user_ID)
        {
            var userToBeDeleted = await dbContext.User.FindAsync(user_ID);

            if (userToBeDeleted != null)
            {
                dbContext.Remove(userToBeDeleted);
                await dbContext.SaveChangesAsync();

                return Ok(userToBeDeleted);
            }

            return NotFound();
        }

        //Felhasználó módosítása
        [HttpPut]
        [Route("UpdateUser/{user_ID:guid}")]
        public async Task<IActionResult> UpdateUser([FromRoute] Guid user_ID, UpdateUserDTO updateUserRequest)
        {
            var userToBeUpdated = await dbContext.User.FindAsync(user_ID);
            CreatePasswordHash(updateUserRequest.password, out byte[] passwordHash, out byte[] passwordSalt);

            if (userToBeUpdated != null)
            {
                userToBeUpdated.userName = updateUserRequest.userName;
                userToBeUpdated.firstName = updateUserRequest.firstName;
                userToBeUpdated.lastName = updateUserRequest.lastName;
                userToBeUpdated.email = updateUserRequest.email;
                userToBeUpdated.sex = updateUserRequest.sex;
                userToBeUpdated.dateOfBirth = updateUserRequest.dateOfBirth;
                userToBeUpdated.PasswordHash = passwordHash;
                userToBeUpdated.PasswordSalt = passwordSalt;

                await dbContext.SaveChangesAsync();

                return Ok(userToBeUpdated);
            }

            return NotFound();
        }

        //SEGÉDFÜGGVÉNYEK
        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }

        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim("Id", user.user_ID.ToString())
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                _configuration.GetSection("Jwt:Key").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }
    }
}
