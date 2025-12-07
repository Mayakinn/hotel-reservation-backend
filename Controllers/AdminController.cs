using System.Data.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using viesbuciu_rezervacija_backend.Models;

namespace viesbuciu_rezervacija_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class AdminController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public AdminController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet("users")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userManager.Users.ToListAsync();

            var result = new List<object>();

            foreach (var u in users)
            {
                var roles = await _userManager.GetRolesAsync(u);

                result.Add(new
                {
                    u.Id,
                    u.Email,
                    u.UserName,
                    Roles = roles // <-- add this
                });
            }

            return Ok(result);
        }

        [HttpPost("promote-to-hotelowner/{userId}")]
        public async Task<IActionResult> PromoteToHotelOwner(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return NotFound();

            await _userManager.RemoveFromRoleAsync(user, "User");
            await _userManager.AddToRoleAsync(user, "HotelOwner");

            return Ok("User promoted to HotelOwner");
        }

        [HttpPost("demote-to-user/{userId}")]
        public async Task<IActionResult> DemoteToUser(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return NotFound();

            await _userManager.RemoveFromRoleAsync(user, "HotelOwner");
            await _userManager.AddToRoleAsync(user, "User");

            return Ok("User demoted to User");
        }
    }
}
