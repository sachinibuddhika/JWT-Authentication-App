using System.ComponentModel.DataAnnotations;

namespace AuthApp.Models
{
    public class User
    {
        [Key]
        public string UserName { get; set; } = string.Empty;
        public string HashedPassword { get; set; } = string.Empty;
    }
}
