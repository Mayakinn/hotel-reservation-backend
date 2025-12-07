using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using viesbuciu_rezervacija_backend.Models;
using Microsoft.EntityFrameworkCore;

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

            var list = new List<object>();

            foreach (var user in users)
            {
                IList<string> roles;

                try
                {
                    roles = await _userManager.GetRolesAsync(user);
                }
                catch
                {
                    roles = new List<string> { "User" }; // fallback to prevent 500 on Azure
                }

                list.Add(new
                {
                    user.Id,
                    user.Email,
                    user.UserName,
                    Roles = roles
                });
            }

            return Ok(list);
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
