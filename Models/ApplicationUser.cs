using Microsoft.AspNetCore.Identity;

namespace viesbuciu_rezervacija_backend.Models
{
    public class ApplicationUser : IdentityUser
    {

        public int? HotelId { get; set; }
        public Hotel? Hotel { get; set; }
    }
}