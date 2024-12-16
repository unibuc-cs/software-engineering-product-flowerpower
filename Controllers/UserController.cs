// using Microsoft.AspNetCore.Mvc;
// using software_engineering_product_flowerpower.Models;
//
// namespace software_engineering_product_flowerpower.Controllers;
//
// [Route("api")]
// [Controller]
// public class UserController : Controller
// {
//     private readonly AppDbContext _context;
//     
//     UserController(AppDbContext context)
//     {
//         _context = context;
//     }
//     
//     public IActionResult Register([FromBody] UserDto user)
//     {
//         if (!ModelState.IsValid)
//         {
//             return BadRequest("Model is not valid");
//         }
//
//         _context.Users.Add(new User());
//
//     }
//     
// }