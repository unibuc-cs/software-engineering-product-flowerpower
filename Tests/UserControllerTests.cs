using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using software_engineering_product_flowerpower.Controllers;
using software_engineering_product_flowerpower.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Xunit;

namespace software_engineering_product_flowerpower.Tests;

public class UserControllerTests
{
    private readonly UserController _controller;
    private readonly AppDbContext _context;

    public UserControllerTests()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "FlowerPowerTest")
            .Options;

        _context = new AppDbContext(options);
        _controller = new UserController(_context);
    }

    [Fact]
    public async Task Register_ValidUser_ReturnsOk()
    {
        var userDto = new UserDto
        {
            Email = "gica@pistolaru.com",
            Password = "gicapistolaru420",
            Username = "GicaPistolaru"
        };

        var result = await _controller.Register(userDto);

        Assert.IsType<OkObjectResult>(result);

        var usersInDb = await _context.Users.CountAsync();
        Assert.Equal(1, usersInDb);
    }

    [Fact]
    public async Task Register_InvalidModel_ReturnsBadRequest()
    {
        _controller.ModelState.AddModelError("Email", "Email is required");
        var userDto = new UserDto
        {
            Password = "gicapistolaru42",
            Username = "GicaPistolaru"
        };

        var result = await _controller.Register(userDto);

        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public async Task Login_ValidCredentials_ReturnsOkWithUserId()
    {
        var passwordHasher = new PasswordHasher<string>();
        var hashedPassword = passwordHasher.HashPassword("gica.pistolaru@gherla.com", "gicapistolaru420");

        _context.Users.Add(new User
        {
            Email = "gica.pistolaru@gherla.com",
            Password = hashedPassword,
            Username = "gicapistolaru420"
        });
        await _context.SaveChangesAsync();

        var loginDto = new UserDto
        {
            Email = "gica.pistolaru@gherla.com",
            Password = "gicapistolaru420"
        };

        var result = await _controller.Login(loginDto);

        var okResult = Assert.IsType<OkObjectResult>(result);
        var response = okResult.Value;

        Assert.NotNull(response);

        var userIdProperty = response.GetType().GetProperty("userId");
        Assert.NotNull(userIdProperty);

        var userId = userIdProperty.GetValue(response);
        Assert.NotNull(userId);
    }


    [Fact]
    public async Task Login_InvalidEmail_ReturnsUnauthorized()
    {
        var loginDto = new UserDto
        {
            Email = "marcel.covrigaru@ciordeala.com",
            Password = "gicapistolaru420"
        };

        var result = await _controller.Login(loginDto);

        Assert.IsType<UnauthorizedObjectResult>(result);
    }

    [Fact]
    public async Task Login_InvalidPassword_ReturnsBadRequest()
    {
        var passwordHasher = new PasswordHasher<string>();
        var hashedPassword = passwordHasher.HashPassword("marcel.covrigaru.de.buzau@ciordeala.com", "corigarudebuzau");

        _context.Users.Add(new User
        {
            Email = "marcel.covrigaru.de.buzau@ciordeala.com",
            Password = hashedPassword,
            Username = "MarcelicaCovrigaru"
        });
        await _context.SaveChangesAsync();

        var loginDto = new UserDto
        {
            Email = "marcel.covrigaru.de.buzau@ciordeala.com",
            Password = "ciolacu2"
        };

        var result = await _controller.Login(loginDto);

        Assert.IsType<BadRequestObjectResult>(result);
    }
}
