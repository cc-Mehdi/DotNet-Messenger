using DataLayer.Models;
using Microsoft.EntityFrameworkCore;


namespace DataLayer.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }

        public DbSet<Users> Users { get; set; }
        public DbSet<Messages> Messages { get; set; }
    }
}
