using Microsoft.EntityFrameworkCore;
using AdMedAPI.Models;

namespace AdMedAPI.Data
{
    // Db context class
    public class ApplicationDbContext : DbContext
    {
        // Database context
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        // Db sets used to hold all information from the database
        public DbSet<Application> Applications { get; set; }
        public DbSet<Medication> Medications { get; set; }
        public DbSet<Resident> Residents { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
