using Microsoft.EntityFrameworkCore;
using viesbuciu_rezervacija_backend.Models;

namespace viesbuciu_rezervacija_backend.Data
{
    public class HotelDbContext(DbContextOptions<HotelDbContext> options) : DbContext(options)
    {
        public DbSet<Hotel> Hotels => Set<Hotel>();
        public DbSet<Review> Reviews => Set<Review>();
        public DbSet<Room> Rooms => Set<Room>();
        public DbSet<RoomPicture> RoomPictures => Set<RoomPicture>();

        public override int SaveChanges()
        {
            var entries = ChangeTracker.Entries<BaseEntity>();

            foreach (var entry in entries)
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedAt = DateTime.UtcNow;
                }
            }

            return base.SaveChanges();
        }
    }


}