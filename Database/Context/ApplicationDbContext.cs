using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Data.SqlTypes;
using users_service.Database.Entities;

namespace users_service.Database.Context
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<User> users { get; set; }
        public DbSet<UsersPerformers> usersPerformers { get; set; }

        public ApplicationDbContext(DbContextOptions options): base(options)
        {
            Database.EnsureCreated();
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseNpgsql(@"Host=localhost;Username=maykl;Password=sandman;Database=users_db");
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
          
            modelBuilder.
                Entity<User>()
                .Property(e => e.Sex)
                .HasConversion(new Microsoft.EntityFrameworkCore.Storage.ValueConversion.EnumToStringConverter<Sex>());

            modelBuilder.
                Entity<User>()
                .Property(e => e.Role)
                .HasConversion(new Microsoft.EntityFrameworkCore.Storage.ValueConversion.EnumToStringConverter<Role>());
        }
    }
}
