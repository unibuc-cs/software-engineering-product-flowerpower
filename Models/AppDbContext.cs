using Microsoft.EntityFrameworkCore;

namespace software_engineering_product_flowerpower.Models;

public class AppDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Photo> Photos { get; set; }
    public DbSet<Visibility> Visibilities { get; set; }
    public DbSet<Notification> Notifications { get; set; }
    public DbSet<FriendRequest> FriendRequests { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // FK compus
        modelBuilder.Entity<Visibility>()
            .HasKey(v => new { v.Photo_ID, v.User_ID });
        
        // trebuie dezactivat cascading delete ca sa nu stearga userii din User, doar friend requestul etc
        modelBuilder.Entity<FriendRequest>()
            .HasOne(fr => fr.User1)
            .WithMany()
            .HasForeignKey(fr => fr.ID_User1)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<FriendRequest>()
            .HasOne(fr => fr.User2)
            .WithMany()
            .HasForeignKey(fr => fr.ID_User2)
            .OnDelete(DeleteBehavior.Restrict);
        
        modelBuilder.Entity<Notification>()
            .HasOne(n => n.User)
            .WithMany()
            .HasForeignKey(n => n.User_ID)
            .OnDelete(DeleteBehavior.Restrict); 

        modelBuilder.Entity<Notification>()
            .HasOne(n => n.Photo)
            .WithMany()
            .HasForeignKey(n => n.Photo_ID)
            .OnDelete(DeleteBehavior.Cascade);
        
        modelBuilder.Entity<Photo>()
            .HasOne(n => n.User)
            .WithMany()
            .HasForeignKey(n => n.User_ID)
            .OnDelete(DeleteBehavior.Restrict); 
        
        modelBuilder.Entity<Visibility>()
            .HasOne(n => n.User)
            .WithMany()
            .HasForeignKey(n => n.User_ID)
            .OnDelete(DeleteBehavior.Restrict); 

        modelBuilder.Entity<Visibility>()
            .HasOne(n => n.Photo)
            .WithMany()
            .HasForeignKey(n => n.Photo_ID)
            .OnDelete(DeleteBehavior.Cascade);

    }
}