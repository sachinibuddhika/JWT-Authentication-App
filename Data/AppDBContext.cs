

using AuthApp.Models;
using Microsoft.EntityFrameworkCore;

namespace AuthApp.Data
{
    public class AppDBContext:DbContext
    {

        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {
        }


        public DbSet<User> Users { get; set; }
    }
}
