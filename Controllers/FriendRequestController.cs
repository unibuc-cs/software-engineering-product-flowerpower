using Microsoft.AspNetCore.Mvc;
using software_engineering_product_flowerpower.Models;
using System.Linq;
using System.Threading.Tasks;

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
                return NotFound();
            }

            friendRequest.IsAccepted = true;

            var user1 = await _context.Users.FindAsync(friendRequest.ID_User1);
            var user2 = await _context.Users.FindAsync(friendRequest.ID_User2);

            if (user1 != null && user2 != null)
            {
                user1.Friends.Add(user2);
                user2.Friends.Add(user1);
            }

            await _context.SaveChangesAsync();

            return Ok(friendRequest);
        }

        [HttpGet("pending")]
        public IActionResult GetPendingRequests(int userId)
        {
            var pendingRequests = _context.FriendRequests
                .Where(fr => fr.ID_User2 == userId && !fr.IsAccepted)
                .ToList();

            return Ok(pendingRequests);
        }
    }
}