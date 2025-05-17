using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class DatingContext : DbContext
    {
        public DatingContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<DatingAppUser> AppUsers { get; set; } = null!;
    }
}
