using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using software_engineering_product_flowerpower.Models;


namespace software_engineering_product_flowerpower.Controllers
{
    [Route("api/photos")]
    [ApiController]
    public class PhotoController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PhotoController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadPhoto([FromForm] int userId, [FromForm] IFormFile file, [FromForm] int groupId)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("No file was uploaded.");
            }

            try
            {
                using var memoryStream = new MemoryStream();
                await file.CopyToAsync(memoryStream);
                byte[] fileBytes = memoryStream.ToArray();

                var user = _context.Users.FirstOrDefault(u => u.ID == userId);
                if (user == null)
                {
                    return NotFound("User not found.");
                }

                ICollection<User> targetUsers;

                if (groupId == -1)
                {
                    // 🔹 Fetch the user's friends
                    var userWithFriends = _context.Users
                        .Include(u => u.Friends)
                        .FirstOrDefault(u => u.ID == userId);

                    targetUsers = userWithFriends?.Friends ?? new List<User>(); // Handle null case
                }
                else
                {
                    // 🔹 Fetch the group members
                    var groupWithMembers = _context.Groups
                        .Include(g => g.Members)
                        .FirstOrDefault(g => g.ID == groupId);

                    targetUsers = groupWithMembers?.Members ?? new List<User>(); // Handle null case
                }
                
                var photo = new Photo
                {
                    User_ID = userId,
                    Blob = fileBytes,
                    UploadTime = DateTime.UtcNow,
                    User = user
                };

                _context.Photos.Add(photo);
                await _context.SaveChangesAsync();
                
                var visibilities = targetUsers.Select(member => new Visibility
                {
                    User_ID = member.ID,
                    Photo_ID = photo.ID
                }).ToList();

                _context.Visibilities.AddRange(visibilities);

                _context.Visibilities.AddRange(visibilities);

                await _context.SaveChangesAsync();

                return Ok(new { message = "Photo uploaded successfully", photoId = photo.ID });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }
    }
}