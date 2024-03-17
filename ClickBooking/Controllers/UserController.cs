using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ClickBooking.Models;
using System.Threading.Tasks;

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
            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, isPersistent: false);
                return Ok();
            }

            return BadRequest(result.Errors);
        }

[HttpPost("login")]
public async Task<IActionResult> Login([FromBody] Login model)
{
    var user = await _userManager.FindByNameAsync(model.Username);
    if (user == null)
    {
        return BadRequest("User not found");
    }

    var result = await _signInManager.PasswordSignInAsync(user, model.Password, isPersistent: false, lockoutOnFailure: false);

    if (result.Succeeded)
    {
        return Ok("User"+user+ "logged in");
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
    }
}