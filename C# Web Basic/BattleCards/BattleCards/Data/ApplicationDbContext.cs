using BattleCards.Models;
using Microsoft.EntityFrameworkCore;

namespace BattleCards.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Card> Cards { get; set; }
        public DbSet<UserCard> UserCards { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(DatabaseConfiguration.ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserCard>(uc =>
            {
                uc.HasKey(x => new { x.UserId, x.CardId });

                uc.HasOne(x => x.User)
                    .WithMany(u => u.UserCards)
                    .HasForeignKey(x => x.UserId)
                    .OnDelete(DeleteBehavior.Restrict);

                uc.HasOne(x => x.Card)
                    .WithMany(c => c.UserCards)
                    .HasForeignKey(x => x.CardId)
                    .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}
