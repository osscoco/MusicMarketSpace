using EFCore.EntityTypeConfigurations;
using Microsoft.EntityFrameworkCore;
using Models.Identity;

namespace EFCore
{
    public class AppDbContext : DbContext
    {
        public DbSet<Choice> Choices { get; set; } = null!;
        public DbSet<SubChoice> SubChoices { get; set; } = null!;
        public DbSet<UserChoice> UserChoices { get; set; } = null!;
        public DbSet<User> Users { get; set; } = null!;
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new SubChoiceConfiguration());
            modelBuilder.ApplyConfiguration(new UserChoiceConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}