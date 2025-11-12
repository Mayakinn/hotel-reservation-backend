using viesbuciu_rezervacija_backend.Models;

namespace viesbuciu_rezervacija_backend.Interfaces
{
    public interface IRefreshTokenRepository
    {
        Task<RefreshToken?> GetByTokenAsync(string token);
        Task AddAsync(RefreshToken token);
        Task RevokeAsync(RefreshToken token);
        Task SaveChangesAsync();
    }
}