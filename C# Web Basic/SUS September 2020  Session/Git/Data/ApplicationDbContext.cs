using Git.Models;
using Microsoft.EntityFrameworkCore;

namespace Git.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Repository> Repositories { get; set; }
        public DbSet<Commit> Commits { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(DatabaseConfiguration.ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Commit>(c =>
            {
                c.HasOne(x => x.Creator)
                    .WithMany(z => z.Commits)
                    .HasForeignKey(x => x.CreatorId)
                    .OnDelete(DeleteBehavior.Restrict);

                c.HasOne(x => x.Repository)
                    .WithMany(z => z.Commits)
                    .HasForeignKey(x => x.RepositoryId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Repository>(r =>
            {
                r.HasOne(x => x.Owner)
                    .WithMany(z => z.Repositories)
                    .HasForeignKey(x => x.OwnerId)
                    .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}
