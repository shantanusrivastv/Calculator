using System.Reflection;
using Microsoft.EntityFrameworkCore;
using UL.Calculator.Entities;

namespace UL.Calculator.Data
{
    public class CalculatorDBContext : DbContext
    {
        public DbSet<User> User { get; set; }
        public DbSet<UserLogin> UserLogin { get; set; }

        public CalculatorDBContext(DbContextOptions<CalculatorDBContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //It searches for all the configuration of all entities
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            modelBuilder.Entity<User>().HasData(
                        new User { Id = 1, FirstName = "Simon ", LastName = "Thompson ", Email = "s.Thompson@ul.com" },
                        new User { Id = 2, FirstName = "Stephanie", LastName = "Gibbens", Email = "s.Gibbens@ul.com" },
                        new User { Id = 3, FirstName = "Free", LastName = "User", Email = "free@free.com" });

            modelBuilder.Entity<UserLogin>().HasData(
                    new UserLogin { Username = "s.Thompson@ul.com", Password = "admin", SubscriptionType = SubscriptionType.Premium },
                    new UserLogin { Username = "s.Gibbens@ul.com", Password = "admin", SubscriptionType = SubscriptionType.Standard },
                    new UserLogin { Username = "free@free.com", Password = "user", SubscriptionType = SubscriptionType.Free });
        }
    }
}