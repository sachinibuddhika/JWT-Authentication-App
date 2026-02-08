using AuthApp.Data;
using AuthApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AuthApp.Services
{
    public class AuthService(AppDBContext dBContext,IConfiguration configuration):IAuthService
    {
   
        async Task<User?> IAuthService.RegisterAsync(UserDTO request)
        {

            if(await dBContext.Users.AnyAsync(u => u.UserName == request.UserName)){
                return null;

            }
            var user=new User();
            var hashedPassword = new PasswordHasher<User>().HashPassword(user, request.Password);
            user.UserName = request.UserName;
            user.HashedPassword = hashedPassword;
          
            dBContext.Users.Add(user);
            await dBContext.SaveChangesAsync();
            return user;
        }
    }
}
