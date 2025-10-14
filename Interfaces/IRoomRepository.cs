using viesbuciu_rezervacija_backend.Models;

namespace viesbuciu_rezervacija_backend.Interfaces
{
    public interface IRoomRepository
    {
        Task<Room> CreateAsync(Room room);
        Task<List<Room>> GetAllAsync();
        Task<Room?> GetByIdAsync(int id, bool includeReviews = false, bool includePictures = false);
        Task<Room?> UpdateAsync(Room room);
        Task<Room?> DeleteAsync(int id);
        Task<List<Room>> GetAllByHotelAsync(int id);
    }
}