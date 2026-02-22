using AuthApp.Data;
using AuthApp.Models;
using AuthApp.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AuthApp.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IAuthService authService,IConfiguration configuration) : ControllerBase
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

        [HttpPost("Login")]
        public async Task<ActionResult<string>> Login(UserDTO request)
        {
            var token=await authService.LoginAsync(request);
            if (token == null)
            {
                return BadRequest("Invalid username or password!");
            }
            else
            {
                return Ok(token);
            }

        }

        
    }
}
