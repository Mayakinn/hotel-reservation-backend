using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using viesbuciu_rezervacija_backend.Models;

namespace viesbuciu_rezervacija_backend.Data
{
    public class HotelDbContext : IdentityDbContext<ApplicationUser>
    {
        public HotelDbContext(DbContextOptions<HotelDbContext> options) : base(options)
        {
        }

        public DbSet<Hotel> Hotels => Set<Hotel>();
        public DbSet<Review> Reviews => Set<Review>();
        public DbSet<Room> Rooms => Set<Room>();
        public DbSet<RoomPicture> RoomPictures => Set<RoomPicture>();
        public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();
        public override int SaveChanges()
        {
            var entries = ChangeTracker.Entries<BaseEntity>();
            foreach (var entry in entries)
            {
                if (entry.State == EntityState.Added)
                    entry.Entity.CreatedAt = DateTime.UtcNow;
            }

            return base.SaveChanges();
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Hotel>()
                .HasOne(h => h.Owner)
                .WithOne(u => u.Hotel)
                .HasForeignKey<Hotel>(h => h.OwnerId);
        }

    }
}
