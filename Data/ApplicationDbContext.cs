using Microsoft.EntityFrameworkCore;
using PasswordService.Models;

namespace PasswordService.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<User> Usuario { get; set; }
    }
}