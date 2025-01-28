using Microsoft.EntityFrameworkCore;

namespace software_engineering_product_flowerpower.Models;

public class AppDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Photo> Photos { get; set; }
    public DbSet<Visibility> Visibilities { get; set; }
    public DbSet<Notification> Notifications { get; set; }
    public DbSet<FriendRequest> FriendRequests { get; set; }
    public DbSet<Group> Groups { get; set; } // Adăugăm această linie

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasIndex(u => u.Email)
            .IsUnique();
        
        modelBuilder.Entity<User>()
            .HasMany(u => u.Friends)
            .WithMany()
            .UsingEntity<Dictionary<string, object>>(
                "UserFriends",
                j => j.HasOne<User>().WithMany().HasForeignKey("FriendId").OnDelete(DeleteBehavior.Restrict),
                j => j.HasOne<User>().WithMany().HasForeignKey("UserId").OnDelete(DeleteBehavior.Restrict)
            );

        modelBuilder.Entity<User>()
            .HasMany(u => u.Groups)
            .WithMany(g => g.Members)
            .UsingEntity<Dictionary<string, object>>(
                "UserGroups",
                j => j.HasOne<Group>().WithMany().HasForeignKey("GroupId").OnDelete(DeleteBehavior.Restrict),
                j => j.HasOne<User>().WithMany().HasForeignKey("UserId").OnDelete(DeleteBehavior.Restrict)
            );

        modelBuilder.Entity<Group>()
            .HasOne(g => g.Owner)
            .WithMany()
            .HasForeignKey(g => g.OwnerId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Visibility>()
            .HasKey(v => new { v.Photo_ID, v.User_ID });
        
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