using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Backend.Models
{
    public class UserContext : DbContext
    {

        public UserContext(DbContextOptions options)
            :base(options)
        {
            
        }

        public DbSet<User> Users { get; set; }
    }
}
