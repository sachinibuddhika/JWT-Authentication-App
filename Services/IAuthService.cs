using AuthApp.Data;
using AuthApp.Models;

namespace AuthApp.Services
{
    public interface IAuthService
    {
        Task<User?> RegisterAsync(UserDTO request);
    }
}
