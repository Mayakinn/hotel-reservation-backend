namespace viesbuciu_rezervacija_backend.Models
{
    public abstract class BaseEntity
    {
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    }
}