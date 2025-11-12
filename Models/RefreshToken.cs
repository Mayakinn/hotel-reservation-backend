using System.Text.Json.Serialization;

namespace viesbuciu_rezervacija_backend.Models
{
    public class RefreshToken
    {
        public int Id { get; set; }
        public string Token { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty;
        public DateTime ExpiresAt { get; set; }
        public bool IsRevoked { get; set; }
        public string? ReplacedByToken { get; set; }
        [JsonIgnore]
        public bool IsExpired => DateTime.UtcNow >= ExpiresAt;
    }
}