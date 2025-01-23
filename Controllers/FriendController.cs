using Microsoft.AspNetCore.Mvc;
using software_engineering_product_flowerpower.Models;
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

        [HttpGet("list")]
        public IActionResult GetFriendsList(int userId)
        {
            var friends = _context.Friends
                .Where(f => f.ID_User1 == userId || f.ID_User2 == userId)
                .Select(f => f.ID_User1 == userId ? f.User2 : f.User1)
                .ToList();

            return Ok(friends);
        }
    }
}