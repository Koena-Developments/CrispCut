using Microsoft.EntityFrameworkCore;

using Trim.Models;

namespace Trim.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        // Add a DbSet for each of your models
        public DbSet<User> Users { get; set; }
        public DbSet<Artist> Artists { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<ArtistService> ArtistServices { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Ride> Rides { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure the one-to-one relationship between User and Artist
            modelBuilder.Entity<User>()
                .HasOne(u => u.ArtistProfile)
                .WithOne(a => a.User)
                .HasForeignKey<Artist>(a => a.UserId);
                
            // Configure the one-to-one relationship between Booking and Review
            modelBuilder.Entity<Booking>()
                .HasOne(b => b.Review)
                .WithOne(r => r.Booking)
                .HasForeignKey<Review>(r => r.BookingId);

            // *** FIX FOR BOOKING CASCADE ISSUE ***
            // This tells EF Core not to create a cascading delete from Artist to Booking,
            // which breaks the cycle. The cascade from User to Booking will still work.
            modelBuilder.Entity<Booking>()
                .HasOne(b => b.Artist)
                .WithMany(a => a.Bookings)
                .HasForeignKey(b => b.ArtistId)
                .OnDelete(DeleteBehavior.Restrict); // Using Restrict is safer

            modelBuilder.Entity<Booking>()
                .HasOne(b => b.User)
                .WithMany(u => u.Bookings)
                .HasForeignKey(b => b.UserId)
                .OnDelete(DeleteBehavior.Restrict); // Also set this one for consistency

            // *** FIX FOR REVIEW CASCADE ISSUE ***
            // Break the cascade paths from User and Artist directly to Review.
            // The relationship from Booking to Review will handle deletion.
            modelBuilder.Entity<Review>()
                .HasOne(r => r.User)
                .WithMany(u => u.Reviews)
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Review>()
                .HasOne(r => r.Artist)
                .WithMany(a => a.Reviews)
                .HasForeignKey(r => r.ArtistId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}

