using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static TOLTournamentLeague.DOM.DTO;
using TOLTournamentLeague.DOM;
using TOLTournamentLeague.UserRepository;

namespace TOLTournamentLeague.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public AuthController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpPost("signup")]
        public async Task<IActionResult> Signup(UserSignupDto userSignupDto)
        {
            if (userSignupDto.Password != userSignupDto.ConfirmPassword)
            {
                return BadRequest("Passwords do not match.");
            }

            using var hmac = new System.Security.Cryptography.HMACSHA512();
            var passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(userSignupDto.Password));
            var passwordSalt = hmac.Key;

            var user = new User
            {
                Username = userSignupDto.Username,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                RoleId = 1 // Default role ID for regular users
            };

            await _userRepository.AddUserAsync(user);
            return Ok("User registered successfully.");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginDto userLoginDto)
        {
            var user = await _userRepository.GetUserByUsernameAsync(userLoginDto.Username);
            if (user == null)
                return Unauthorized("Invalid username or password.");

            using var hmac = new System.Security.Cryptography.HMACSHA512(user.PasswordSalt);
            var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(userLoginDto.Password));

            if (!computedHash.SequenceEqual(user.PasswordHash))
                return Unauthorized("Invalid username or password.");

            // Generate a token (not shown here, for simplicity)
            var token = GenerateToken(user);

            return Ok(new { Token = token });
        }

        private string GenerateToken(User user)
        {
            // Token generation logic (not implemented here for brevity)
            return "GeneratedToken";
        }
    }
}
