using SharedTrip.Models;

namespace SharedTrip.Data
{
    using Microsoft.EntityFrameworkCore;

    public class ApplicationDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Trip> Trips { get; set; }
        public DbSet<UserTrip> UserTrips { get; set; }

        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions db)
            : base(db)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(
                    @"Server=ALEKSANDRA\SQLEXPRESS;Database=SharedTrip;Integrated Security=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserTrip>(ut =>
            {
                ut.HasKey(x => new { x.UserId, x.TripId });

                ut.HasOne(x => x.User)
                    .WithMany(y => y.UserTrips)
                    .HasForeignKey(x => x.UserId)
                    .OnDelete(DeleteBehavior.Restrict);

                ut.HasOne(x => x.Trip)
                    .WithMany(y => y.UserTrips)
                    .HasForeignKey(x => x.TripId)
                    .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}
