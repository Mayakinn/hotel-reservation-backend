using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Security.Cryptography;
using viesbuciu_rezervacija_backend.DTOs;
using viesbuciu_rezervacija_backend.Interfaces;
using viesbuciu_rezervacija_backend.Models;
using viesbuciu_rezervacija_backend.Repository;

namespace viesbuciu_rezervacija_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IRefreshTokenRepository _refreshTokenRepo;
        private readonly TokenService _jwtService;

        public AuthController(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IRefreshTokenRepository refreshTokenRepo,
            TokenService jwtService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _refreshTokenRepo = refreshTokenRepo;
            _jwtService = jwtService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null || !await _userManager.CheckPasswordAsync(user, model.Password))
                return Unauthorized("Invalid credentials");

            var roles = await _userManager.GetRolesAsync(user);
            var accessToken = _jwtService.GenerateJwtToken(user, roles);

            var refreshToken = new RefreshToken
            {
                Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                UserId = user.Id,
                ExpiresAt = DateTime.UtcNow.AddDays(7)
            };

            await _refreshTokenRepo.AddAsync(refreshToken);
            await _refreshTokenRepo.SaveChangesAsync();

            Response.Cookies.Append("refreshToken", refreshToken.Token, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = refreshToken.ExpiresAt
            });

            return Ok(new { accessToken });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto model)
        {
            var user = new ApplicationUser
            {
                UserName = model.Username,
                Email = model.Email
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                return BadRequest(result.Errors);

            if (!await _roleManager.RoleExistsAsync("User"))
                await _roleManager.CreateAsync(new IdentityRole("User"));

            await _userManager.AddToRoleAsync(user, "User");

            return Ok("User registered successfully");
        }

        [HttpPost("register-hotelowner")]
        public async Task<IActionResult> RegisterHotelOwner([FromBody] RegisterHotelOwnerDto model)
        {
            var user = new ApplicationUser
            {
                UserName = model.Username,
                Email = model.Email
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                return BadRequest(result.Errors);

            if (!await _roleManager.RoleExistsAsync("HotelOwner"))
                await _roleManager.CreateAsync(new IdentityRole("HotelOwner"));

            await _userManager.AddToRoleAsync(user, "HotelOwner");

            var roles = await _userManager.GetRolesAsync(user);
            var accessToken = _jwtService.GenerateJwtToken(user, roles);

            var refreshToken = new RefreshToken
            {
                Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                UserId = user.Id,
                ExpiresAt = DateTime.UtcNow.AddDays(7)
            };

            await _refreshTokenRepo.AddAsync(refreshToken);
            await _refreshTokenRepo.SaveChangesAsync();

            Response.Cookies.Append("refreshToken", refreshToken.Token, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = refreshToken.ExpiresAt
            });

            return Ok(new { accessToken });
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh()
        {
            var refreshToken = Request.Cookies["refreshToken"];
            if (string.IsNullOrEmpty(refreshToken))
                return Unauthorized("Missing refresh token");

            var storedToken = await _refreshTokenRepo.GetByTokenAsync(refreshToken);
            if (storedToken == null || storedToken.IsExpired || storedToken.IsRevoked)
                return Unauthorized("Invalid refresh token");

            var user = await _userManager.FindByIdAsync(storedToken.UserId);
            if (user == null)
                return Unauthorized("User not found");

            var roles = await _userManager.GetRolesAsync(user);
            var newAccessToken = _jwtService.GenerateJwtToken(user, roles);

            var newRefreshToken = new RefreshToken
            {
                Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                UserId = user.Id,
                ExpiresAt = DateTime.UtcNow.AddSeconds(30)
            };

            storedToken.IsRevoked = true;
            storedToken.ReplacedByToken = newRefreshToken.Token;

            await _refreshTokenRepo.AddAsync(newRefreshToken);
            await _refreshTokenRepo.SaveChangesAsync();

            Response.Cookies.Append("refreshToken", newRefreshToken.Token, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = newRefreshToken.ExpiresAt
            });

            return Ok(new { accessToken = newAccessToken });
        }

        [Authorize]
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            var refreshToken = Request.Cookies["refreshToken"];
            if (!string.IsNullOrEmpty(refreshToken))
            {
                var token = await _refreshTokenRepo.GetByTokenAsync(refreshToken);
                if (token != null)
                {
                    token.IsRevoked = true;
                    await _refreshTokenRepo.SaveChangesAsync();
                }

                Response.Cookies.Delete("refreshToken");
            }

            return Ok("Logged out successfully");
        }
    }
}
