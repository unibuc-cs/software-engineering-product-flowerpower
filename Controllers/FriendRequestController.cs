using Microsoft.AspNetCore.Mvc;
using software_engineering_product_flowerpower.Models;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace software_engineering_product_flowerpower.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FriendRequestController : ControllerBase
    {
        private readonly AppDbContext _context;

        public FriendRequestController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("send")]
        public async Task<IActionResult> SendFriendRequest(int senderId, int receiverId)
        {
            var sender = await _context.Users.Include(u => u.Friends).FirstOrDefaultAsync(u => u.ID == senderId);
            var receiver = await _context.Users.Include(u => u.Friends).FirstOrDefaultAsync(u => u.ID == receiverId);

            if (sender == null || receiver == null)
            {
                return NotFound("One or both users not found");
            }

            
            var existingRequest = await _context.FriendRequests
                .FirstOrDefaultAsync(fr => (fr.ID_User1 == senderId && fr.ID_User2 == receiverId) || (fr.ID_User1 == receiverId && fr.ID_User2 == senderId));

            if (existingRequest != null)
            {
                return BadRequest("Friend request already exists");
            }

            
            if (sender.Friends.Any(f => f.ID == receiverId))
            {
                return BadRequest("Users are already friends");
            }

            var friendRequest = new FriendRequest
            {
                ID_User1 = senderId,
                ID_User2 = receiverId,
                RequestDate = DateTime.Now,
                IsAccepted = false
            };

            _context.FriendRequests.Add(friendRequest);
            await _context.SaveChangesAsync();

            return Ok(friendRequest);
        }

        [HttpPost("accept")]
        public async Task<IActionResult> AcceptFriendRequest(int requestId)
        {
            var friendRequest = await _context.FriendRequests.FindAsync(requestId);
            if (friendRequest == null)
            {
                return NotFound("Friend request not found");
            }

            friendRequest.IsAccepted = true;

            var user1 = await _context.Users.Include(u => u.Friends).FirstOrDefaultAsync(u => u.ID == friendRequest.ID_User1);
            var user2 = await _context.Users.Include(u => u.Friends).FirstOrDefaultAsync(u => u.ID == friendRequest.ID_User2);

            if (user1 == null || user2 == null)
            {
                return NotFound("One or both users not found");
            }

            if (!user1.Friends.Any(f => f.ID == user2.ID))
            {
                user1.Friends.Add(user2);
            }

            if (!user2.Friends.Any(f => f.ID == user1.ID))
            {
                user2.Friends.Add(user1);
            }

            await _context.SaveChangesAsync();

            return Ok(friendRequest);
        }

        [HttpGet("all")]
        public IActionResult GetAllFriendRequests(int userId, bool? isAccepted = null)
        {
            var query = _context.FriendRequests.AsQueryable();

            query = query.Where(fr => fr.ID_User2 == userId);

            if (isAccepted.HasValue)
            {
                query = query.Where(fr => fr.IsAccepted == isAccepted.Value);
            }

            var friendRequests = query
                .Select(fr => new FriendRequestDto
                {
                    ID = fr.ID,
                    SenderUsername = _context.Users.Where(u => u.ID == fr.ID_User1).Select(u => u.Username).FirstOrDefault() ?? "Unknown",
                    ReceiverUsername = _context.Users.Where(u => u.ID == fr.ID_User2).Select(u => u.Username).FirstOrDefault() ?? "Unknown",
                    RequestDate = fr.RequestDate,
                    IsAccepted = fr.IsAccepted
                })
                .ToList();

            return Ok(friendRequests);
        }
    }
}