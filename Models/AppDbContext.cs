using Microsoft.EntityFrameworkCore;

namespace software_engineering_product_flowerpower.Models;

public class AppDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Photo> Photos { get; set; }
    public DbSet<Visibility> Visibilities { get; set; }
    public DbSet<Notification> Notifications { get; set; }
    public DbSet<FriendRequest> FriendRequests { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("YourConnectionStringHere");
    }
}