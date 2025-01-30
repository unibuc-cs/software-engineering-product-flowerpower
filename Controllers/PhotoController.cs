using Microsoft.AspNetCore.Mvc;
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
        public async Task<IActionResult> UploadPhoto([FromForm] int userId, [FromForm] IFormFile file)
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

                var photo = new Photo
                {
                    User_ID = userId,
                    Blob = fileBytes,
                    UploadTime = DateTime.UtcNow,
                    User = user
                };

                _context.Photos.Add(photo);
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