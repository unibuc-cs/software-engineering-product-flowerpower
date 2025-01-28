using Microsoft.AspNetCore.Mvc;
using software_engineering_product_flowerpower.Models;
using Microsoft.EntityFrameworkCore;

namespace software_engineering_product_flowerpower.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GroupController : ControllerBase
{
    private readonly AppDbContext _context;

    public GroupController(AppDbContext context)
    {
        _context = context;
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateGroup([FromBody] GroupDto groupDto)
    {
        var owner = await _context.Users.FindAsync(groupDto.OwnerId);
        if (owner == null)
        {
            return NotFound("Owner not found");
        }

        var group = new Group
        {
            Name = groupDto.Name,
            OwnerId = groupDto.OwnerId,
            Owner = owner
        };

        _context.Groups.Add(group);
        await _context.SaveChangesAsync();

        return Ok(group);
    }

    [HttpPost("{groupId}/add-member")]
    public async Task<IActionResult> AddMember(int groupId, [FromBody] int userId)
    {
        var group = await _context.Groups.Include(g => g.Members).FirstOrDefaultAsync(g => g.ID == groupId);
        if (group == null)
        {
            return NotFound("Group not found");
        }

        var user = await _context.Users.FindAsync(userId);
        if (user == null)
        {
            return NotFound("User not found");
        }

        group.Members.Add(user);
        await _context.SaveChangesAsync();

        return Ok(group);
    }
    
    [HttpGet("user/{userId}/groups")]
    public async Task<IActionResult> GetUserGroups(int userId)
    {
        var groups = await _context.Groups
            .Where(g => g.OwnerId == userId)
            .ToListAsync();

        return Ok(groups);
    }
    [HttpGet("{groupId}")]
    public async Task<IActionResult> GetGroupDetails(int groupId)
    {
        var group = await _context.Groups
            .Include(g => g.Members)
            .FirstOrDefaultAsync(g => g.ID == groupId);

        if (group == null)
        {
            return NotFound("Group not found");
        }

        return Ok(group);
    }
}