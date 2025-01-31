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
        
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetUserPhotos(int userId)
        {
            try
            {
                var user = await _context.Users.FindAsync(userId);
                if (user == null)
                {
                    return NotFound("User not found.");
                }

                var visiblePhotos = await _context.Visibilities
                    .Where(v => v.User_ID == userId)
                    .Include(v => v.Photo)
                    .ThenInclude(p => p.User) 
                    .Select(v => new 
                    {
                        id = v.Photo.ID,
                        username = v.Photo.User.Username, 
                        blob = Convert.ToBase64String(v.Photo.Blob),
                        uploadTime = v.Photo.UploadTime
                    })
                    .Where(p => p.uploadTime >= DateTime.UtcNow.AddHours(-24))
                    .ToListAsync();


                if (!visiblePhotos.Any())
                {
                    return NotFound("No recent photos found.");
                }

                return Ok(visiblePhotos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }
        
        [HttpGet("uploaded/{userId}")]
        public async Task<IActionResult> GetUserUploadedPhotos(int userId)
        {
            try
            {
                var user = await _context.Users.FindAsync(userId);
                if (user == null)
                {
                    return NotFound("User not found.");
                }

                var uploadedPhotos = await _context.Photos
                    .Where(p => p.User_ID == userId) // ✅ Get only the user's uploaded photos
                    .Where(p => p.UploadTime >= DateTime.UtcNow.AddHours(-24)) // ✅ Filter last 24h
                    .Select(p => new 
                    {
                        id = p.ID,
                        username = p.User.Username, // ✅ Get username
                        blob = Convert.ToBase64String(p.Blob),
                        uploadTime = p.UploadTime
                    })
                    .ToListAsync();

                if (!uploadedPhotos.Any())
                {
                    return NotFound("No recent photos uploaded by this user.");
                }

                return Ok(uploadedPhotos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

    }
}