using Microsoft.EntityFrameworkCore;
using viesbuciu_rezervacija_backend.Data;
using viesbuciu_rezervacija_backend.Interfaces;
using viesbuciu_rezervacija_backend.Mappers;
using viesbuciu_rezervacija_backend.Models;

namespace viesbuciu_rezervacija_backend.Repository
{
    public class HotelRepository : IHotelRepository
    {
        private readonly HotelDbContext _context;

        public HotelRepository(HotelDbContext context)
        {
            _context = context;
        }

        public async Task<Hotel> CreateAsync(Hotel hotelModel)
        {
            await _context.Hotels.AddAsync(hotelModel);
            await _context.SaveChangesAsync();
            return hotelModel;
        }

        public async Task<Hotel?> DeleteHotelAsync(int id)
        {
            var hotelModel = await _context.Hotels.FirstOrDefaultAsync(x => x.Id == id);
            if (hotelModel == null)
            {
                return null;
            }
            _context.Hotels.Remove(hotelModel);
            await _context.SaveChangesAsync();
            return hotelModel;
        }

        public async Task<Hotel?> GetByIdAsync(int id)
        {
            var hotelModel = await _context.Hotels.Include(r => r.Rooms).FirstOrDefaultAsync(x => x.Id == id);
            if (hotelModel == null)
            {
                return null;
            }
            return hotelModel;
        }

        public async Task<List<Hotel>> GetHotelsAsync()
        {
            return await _context.Hotels.Include(r => r.Rooms).ToListAsync();
        }

        public Task<bool> HotelExists(int id)
        {
            return _context.Hotels.AnyAsync(h => h.Id == id);
        }

        public async Task<Hotel?> UpdateHotel(int id, UpdateHotelRequestDto hotelDto)
        {
            var hotelModel = await _context.Hotels.FirstOrDefaultAsync(h => h.Id == id);
            if (hotelModel == null)
            {
                return null;
            }

            hotelModel.Name = hotelDto.Name;
            hotelModel.Description = hotelDto.Description;
            hotelModel.PhoneNumber = hotelDto.PhoneNumber;
            hotelModel.City = hotelDto.City;
            hotelModel.Street = hotelDto.Street;
            hotelModel.StreetNumber = hotelDto.StreetNumber;
            hotelModel.Parking = hotelDto.Parking;
            hotelModel.Breakfast = hotelDto.Breakfast;
            hotelModel.Type = hotelDto.Type;
            await _context.SaveChangesAsync();
            return hotelModel;
        }
    }
}