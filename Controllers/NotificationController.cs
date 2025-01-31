using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using software_engineering_product_flowerpower.Models;

namespace software_engineering_product_flowerpower.Controllers;

[Route("api/notifications")]
[ApiController]
public class NotificationController : ControllerBase
{
    private readonly AppDbContext _context;

    public NotificationController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet("get-notifications/{userId}")]
    public async Task<IActionResult> GetNotifications(int userId)
    {
        try
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null)
            {
                return NotFound("User not found.");
            }

            var notifications = await _context.Notifications
                .Where(v => v.User_ID == userId)
                .Include(v => v.Photo)
                .ThenInclude(p => p.User) 
                .Select(v => new 
                {
                    username = v.Photo.User.Username, 
                    uploadTime = v.Photo.UploadTime
                })
                .Where(p => p.uploadTime >= DateTime.UtcNow.AddHours(-24))
                .ToListAsync();


            if (!notifications.Any())
            {
                return NotFound("No notifications yet!");
            }

            return Ok(notifications);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal Server Error: {ex.Message}");
        }

    }
}
