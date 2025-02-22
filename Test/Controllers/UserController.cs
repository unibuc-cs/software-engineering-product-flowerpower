using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using software_engineering_product_flowerpower.Test.Models;

namespace software_engineering_product_flowerpower.Test.Controllers;

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
            return Ok("User registered successfully");
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
            return Unauthorized("Invalid credentials");
        }
        
        if (_passwordHasher.VerifyHashedPassword(userDto.Email!, user.Password, userDto.Password!) ==
            PasswordVerificationResult.Success)
        {
            return Ok(new { userId = user.ID });
        }

        return BadRequest("Failed to log user in");

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