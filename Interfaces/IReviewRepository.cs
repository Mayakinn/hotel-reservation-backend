using viesbuciu_rezervacija_backend.Models;

namespace viesbuciu_rezervacija_backend.Interfaces
{
    public interface IReviewRepository
    {
        Task<IEnumerable<Review>> GetByRoomIdAsync(int roomId);
        Task<Review?> GetByIdAsync(int id);
        Task<Review> CreateAsync(Review review);
        Task DeleteAsync(Review review);
        Task<Review?> UpdateAsync(Review review);

    }

}