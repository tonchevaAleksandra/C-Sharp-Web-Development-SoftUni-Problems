﻿using IRunes.Models;
using Microsoft.EntityFrameworkCore;

namespace IRunes.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Track> Tracks { get; set; }
        public DbSet<Album> Albums { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Server=ALEKSANDRA\SQLEXPRESS;Database=IRunes;Integrated Security=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Track>(t =>
            {
                t.HasOne(x => x.Album)
                    .WithMany(y => y.Tracks)
                    .HasForeignKey(x => x.AlbumId)
                    .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}
