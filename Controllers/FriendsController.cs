
using Microsoft.AspNetCore.Mvc;
using software_engineering_product_flowerpower.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace software_engineering_product_flowerpower.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FriendController : ControllerBase
    {
        private readonly AppDbContext _context;

        public FriendController(AppDbContext context)
        {
            _context = context;
        }

        
        [HttpGet("all")]
        public IActionResult GetAllUsers()
        {
            var users = _context.Users.ToList();
            return Ok(users);
        }
         [HttpGet("search")]
        public IActionResult SearchUsers(string username)
        {
            var users = _context.Users
                .Where(u => u.Username.Contains(username))
                .ToList();
            return Ok(users);
        }
    
        [HttpGet("{userId}/friends")]
        public IActionResult GetFriendsList(int userId)
        {
            var user = _context.Users
                .Include(u => u.Friends)
                .FirstOrDefault(u => u.ID == userId);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user.Friends);
        }

       
    }
}




