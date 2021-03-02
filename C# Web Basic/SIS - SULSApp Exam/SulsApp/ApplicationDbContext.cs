using Microsoft.EntityFrameworkCore;
using SulsApp.Models;

namespace SulsApp
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Problem> Problems { get; set; }
        public DbSet<Submission> Submissions { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=ALEKSANDRA\\SQLEXPRESS;Database=SULS;Integrated Security=true");
            }

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Submission>(sub =>
            {
                sub.HasOne(x => x.Problem)
                    .WithMany(y => y.Submissions)
                    .HasForeignKey(x => x.ProblemId)
                    .OnDelete(DeleteBehavior.Restrict);

                sub.HasOne(x => x.User)
                    .WithMany(y => y.Submissions)
                    .HasForeignKey(x => x.UserId)
                    .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}
