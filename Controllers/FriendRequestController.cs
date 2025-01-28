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
            var sender = await _context.Users.FindAsync(senderId);
            var receiver = await _context.Users.FindAsync(receiverId);

            if (sender == null || receiver == null)
            {
                return NotFound("One or both users not found");
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
            Console.WriteLine($"Accepting friend request with ID: {requestId}");

            var friendRequest = await _context.FriendRequests.FindAsync(requestId);
            if (friendRequest == null)
            {
                Console.WriteLine("Friend request not found");
                return NotFound("Friend request not found");
            }

            Console.WriteLine("Friend request found");

            friendRequest.IsAccepted = true;

            var user1 = await _context.Users.Include(u => u.Friends).FirstOrDefaultAsync(u => u.ID == friendRequest.ID_User1);
            var user2 = await _context.Users.Include(u => u.Friends).FirstOrDefaultAsync(u => u.ID == friendRequest.ID_User2);

            if (user1 == null || user2 == null)
            {
                Console.WriteLine("One or both users not found");
                return NotFound("One or both users not found");
            }

            Console.WriteLine($"User1: {user1.Username}, User2: {user2.Username}");

            if (!user1.Friends.Any(f => f.ID == user2.ID))
            {
                user1.Friends.Add(user2);
                Console.WriteLine($"Added {user2.Username} to {user1.Username}'s friends list");
            }

            if (!user2.Friends.Any(f => f.ID == user1.ID))
            {
                user2.Friends.Add(user1);
                Console.WriteLine($"Added {user1.Username} to {user2.Username}'s friends list");
            }

            await _context.SaveChangesAsync();
            Console.WriteLine("Changes saved to the database");

            var friendRequestDto = new FriendRequestDto
            {
                ID = friendRequest.ID,
                SenderUsername = user1.Username,
                ReceiverUsername = user2.Username,
                RequestDate = friendRequest.RequestDate,
                IsAccepted = friendRequest.IsAccepted
            };

            Console.WriteLine("Returning FriendRequestDto");
            return Ok(friendRequestDto);
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