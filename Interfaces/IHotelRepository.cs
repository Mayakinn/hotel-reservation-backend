using viesbuciu_rezervacija_backend.Models;

namespace viesbuciu_rezervacija_backend.Interfaces
{
    public interface IHotelRepository
    {
        Task<List<Hotel>> GetHotelsAsync();
        Task<Hotel?> GetByIdAsync(int id);
        Task<Hotel> CreateAsync(Hotel hotelModel);
        Task<Hotel?> UpdateHotel(int id, UpdateHotelRequestDto hotelDto);
        Task<Hotel?> DeleteHotelAsync(int id);
        Task<bool> HotelExists(int id);


    }
}