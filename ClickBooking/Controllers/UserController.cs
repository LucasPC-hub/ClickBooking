using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ClickBooking.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ClickBooking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public UserController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] Register model)
        {
            var user = new User { UserName = model.Username };

            // Se o modelo incluir um RestauranteId, assuma que o usuário é um gerente
            if (model.RestauranteId.HasValue)
            {
                user.RestauranteId = model.RestauranteId.Value;
            }

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                // Se o usuário é um gerente, adicione-o ao role "Gerente"
                if (model.RestauranteId.HasValue)
                {
                    await _userManager.AddToRoleAsync(user, "Gerente");
                }

                await _signInManager.SignInAsync(user, isPersistent: false);
                return Ok();
            }

            return BadRequest(result.Errors);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] Login model)
        {
            var user = await _userManager.Users
                .Include(u => u.Restaurante) // Carrega o restaurante do usuário
                .FirstOrDefaultAsync(u => u.UserName == model.Username);
            if (user == null)
            {
                return BadRequest("User not found");
            }

            var result = await _signInManager.PasswordSignInAsync(user, model.Password, isPersistent: false, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                // Return the user's ID, username and restaurant
                return Ok(new { Id = user.Id, Username = user.UserName, Restaurante = user.Restaurante });
            }
            else if (result.IsLockedOut)
            {
                return BadRequest("User account locked out");
            }
            else if (result.IsNotAllowed)
            {
                return BadRequest("User is not allowed to sign in");
            }
            else if (result.RequiresTwoFactor)
            {
                return BadRequest("User is required to provide a second factor for authentication");
            }

            return Unauthorized();
        }
        
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Ok();
        }
        [HttpGet("users")]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _userManager.Users.ToListAsync();
            return Ok(users);
        }
        
        [HttpGet("current")]
        public async Task<IActionResult> GetCurrent()
        {
            // Get the user ID
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return Unauthorized();
            }

            // Get the user from the database
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound("User not found");
            }

            // Return the user's ID and username
            return Ok(new { Id = user.Id, Username = user.UserName });
        }
    }
}