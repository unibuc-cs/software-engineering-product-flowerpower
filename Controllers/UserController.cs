using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using software_engineering_product_flowerpower.Models;

namespace software_engineering_product_flowerpower.Controllers;

[Route("api")]
[Controller]
public class UserController : Controller
{
    private readonly AppDbContext _context;
    private readonly PasswordHasher<string> _passwordHasher = new PasswordHasher<string>();
    
    public UserController(AppDbContext context)
    {
        _context = context;
    }

    [Route("register")]
    [HttpPost]
    public async Task<IActionResult> Register([FromBody] UserDto user)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest("Model is not valid");
        }

        await _context.Users.AddAsync(new User
        {
            Email = user.Email,
            Password = _passwordHasher.HashPassword(user.Email, user.Password),
            Username = user.Username
        });

        if (await _context.SaveChangesAsync() > 0)
        {
            return StatusCode(200);
        }

        return BadRequest("Failed to register user");

    }
    
    [Route("login")]
    [HttpPost]
    public async Task<IActionResult> Login([FromBody] UserDto userDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest("Model is not valid");
        }

        var user = _context.Users.FirstOrDefaultAsync(u => u.Email == userDto.Email).Result;
        if (user == null)
        {
            return Unauthorized("Invalid mail address");
        }
        
        if (_passwordHasher.VerifyHashedPassword(userDto.Email!, user.Password, userDto.Password!) ==
            PasswordVerificationResult.Success)
        {
            return Ok(new { userId = user.ID , username = user.Username });
        }

        return BadRequest("Invalid password");

    }
    
    [HttpGet("get-user/{userId}")]
    public async Task<IActionResult> GetUserName(int userId)
    {
        var user = await _context.Users
            .Where(u => u.ID == userId)
            .FirstOrDefaultAsync();

        if (user == null)
        {
            return NotFound();
        }

        return Ok(new { username = user.Username });
    }

    [HttpGet("search")]
        public IActionResult SearchUsers(string username)
        {
            var users = _context.Users
                .Where(u => u.Username.Contains(username))
                .ToList();
            return Ok(users);
        }
    
}