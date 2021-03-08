using Microsoft.EntityFrameworkCore;
using Panda.Models;

namespace Panda.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Package> Packages { get; set; }
        public DbSet<Receipt> Receipts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(DatabaseConfiguration.ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Receipt>(r =>
            {
                r.HasOne(x => x.Recipient)
                    .WithMany(p => p.Receipts)
                    .HasForeignKey(x => x.RecipientId)
                    .OnDelete(DeleteBehavior.Restrict);
                r.HasOne(x => x.Package);

            });

            modelBuilder.Entity<Package>(p =>
            {
                p.HasOne(x => x.Recipient)
                    .WithMany(u => u.Packages)
                    .HasForeignKey(x => x.RecipientId)
                    .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}
