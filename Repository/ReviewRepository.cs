using Microsoft.EntityFrameworkCore;
using viesbuciu_rezervacija_backend.Data;
using viesbuciu_rezervacija_backend.Interfaces;
using viesbuciu_rezervacija_backend.Models;

namespace viesbuciu_rezervacija_backend.Repository
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly HotelDbContext _context;
        public ReviewRepository(HotelDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Review>> GetByRoomIdAsync(int roomId)
        {
            return await _context.Reviews
                .Where(r => r.RoomId == roomId)
                .ToListAsync();
        }

        public async Task<Review?> GetByIdAsync(int id)
        {
            return await _context.Reviews.FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<Review> CreateAsync(Review review)
        {
            await _context.Reviews.AddAsync(review);
            await _context.SaveChangesAsync();
            return review;
        }

        public async Task DeleteAsync(Review review)
        {
            _context.Reviews.Remove(review);
            await _context.SaveChangesAsync();
        }
        public async Task<Review?> UpdateAsync(Review review)
        {
            var existingReview = await _context.Reviews.FirstOrDefaultAsync(r => r.Id == review.Id);
            if (existingReview == null)
                return null;

            existingReview.Comment = review.Comment;
            existingReview.Rating = review.Rating;

            await _context.SaveChangesAsync();
            return existingReview;
        }

    }
}
