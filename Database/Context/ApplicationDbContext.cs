using Microsoft.EntityFrameworkCore;
using users_service.Database.Entities;

namespace users_service.Database.Context
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public ApplicationDbContext(DbContextOptions options): base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(@"Host=localhost;Username=maykl;Password=sandman;Database=users_db");
        }
    }
}
