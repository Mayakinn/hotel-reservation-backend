using Microsoft.EntityFrameworkCore;
using viesbuciu_rezervacija_backend.Data;
using viesbuciu_rezervacija_backend.Interfaces;
using viesbuciu_rezervacija_backend.Models;

namespace viesbuciu_rezervacija_backend.Repository
{
    public class RoomRepository : IRoomRepository
    {
        private readonly HotelDbContext _context;
        public RoomRepository(HotelDbContext context)
        {
            _context = context;
        }

        public async Task<Room> CreateAsync(Room room)
        {
            await _context.Rooms.AddAsync(room);
            await _context.SaveChangesAsync();
            return room;
        }

        public async Task<List<Room>> GetAllAsync()
        {
            return await _context.Rooms.ToListAsync();
        }

        public async Task<Room?> GetByIdAsync(int id, bool includeReviews = false, bool includePictures = false)
        {
            IQueryable<Room> query = _context.Rooms;

            if (includeReviews)
                query = query.Include(r => r.Reviews);

            if (includePictures)
                query = query.Include(r => r.Pictures);

            return await query.FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<List<Room>> GetAllByHotelAsync(int hotelId)
        {
            return await _context.Rooms
                .Where(r => r.HotelId == hotelId)
                .ToListAsync();
        }


        public async Task<Room?> UpdateAsync(Room room)
        {
            var existingRoom = await _context.Rooms.FirstOrDefaultAsync(r => r.Id == room.Id);
            if (existingRoom == null)
                return null;

            existingRoom.NumberOfBeds = room.NumberOfBeds;
            existingRoom.SquareMeters = room.SquareMeters;
            existingRoom.Toileteries = room.Toileteries;

            await _context.SaveChangesAsync();
            return existingRoom;
        }

        public async Task<Room?> DeleteAsync(int id)
        {
            var room = await _context.Rooms.FirstOrDefaultAsync(r => r.Id == id);
            if (room == null)
                return null;

            _context.Rooms.Remove(room);
            await _context.SaveChangesAsync();
            return room;
        }

    }
}