using AuthApp.Data;
using AuthApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AuthApp.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public static User user = new();

        [HttpPost("register")]
        public ActionResult<User> Register(UserDTO result)
        {
            var hashedPassword = new PasswordHasher<User>().HashPassword(user, result.Password);
            user.UserName = result.UserName;
            user.HashedPassword = hashedPassword;
            Console.WriteLine("User Password is...   ",user.HashedPassword);
            return Ok(user);
        }
    }
}
