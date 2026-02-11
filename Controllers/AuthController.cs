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
    public class AuthController(IAuthService authService,IConfiguration configuration,AppDBContext context) : ControllerBase
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
        public ActionResult<string> Login(UserDTO request)
        {
            user = context.Users.FirstOrDefault(u => u.UserName == request.UserName);

            if (user.UserName != request.UserName)
            {
               
                return BadRequest("User Not found");
            }
           
            if(new PasswordHasher<User>().VerifyHashedPassword(user, user.HashedPassword, request.Password)==PasswordVerificationResult.Failed){
            return BadRequest("Wrong Password");
            }

            string token = CreateToken(user);
            return Ok(token);
        }

        private string CreateToken(User user)
        {
            var claims = new List<Claim>
            {new Claim(ClaimTypes.Name, user.UserName),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetValue<string>("AppSettings:Token")!));
        
            var creds=new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

            var tokenDescriptor = new JwtSecurityToken(
                issuer: configuration.GetValue<string>("AppSettings:Issuer"),
                audience: configuration.GetValue<string>("AppSettings:Audience"),
                claims: claims,
                expires: DateTime.UtcNow.AddDays(1),
                signingCredentials: creds
                );

            return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);

        }
    }
}
