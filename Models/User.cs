namespace software_engineering_product_flowerpower.Models;

public class User
{
    public int ID { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }

    public virtual ICollection<User> Friends { get; set; }

    public virtual ICollection<User> GroupMembers { get; set; }

    public virtual ICollection<Photo> Photos { get; set; }
}