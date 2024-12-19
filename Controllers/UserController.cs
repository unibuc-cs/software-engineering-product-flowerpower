using Microsoft.AspNetCore.Mvc;
using software_engineering_product_flowerpower.Models;

namespace software_engineering_product_flowerpower.Controllers;

[Route("api")]
[Controller]
public class UserController : Controller
{
    private readonly AppDbContext _context;
    
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
            Password = user.Password,
            Username = user.Username
        });

        if (await _context.SaveChangesAsync() > 0)
        {
            return Ok("User registered successfully");
        }

        return BadRequest("Failed to register user");

    }
    
}