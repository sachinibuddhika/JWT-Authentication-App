using AuthApp.Data;
using AuthApp.Models;
using AuthApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace AuthApp.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IAuthService authService) : ControllerBase
    {
        public static User user = new();

        [HttpPost("register")]
        public async Task<ActionResult<User>> RegisterAsync(UserDTO request)
        {
            var user= await authService.RegisterAsync(request);
            if (user == null)
            {
                return BadRequest("User already exists");
            }
            return Ok(user);
        }
    }
}
