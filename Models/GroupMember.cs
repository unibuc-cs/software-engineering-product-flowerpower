namespace software_engineering_product_flowerpower.Models;
public class GroupMember
{
    public int GroupId { get; set; }
    public Group Group { get; set; }

    public int UserId { get; set; }
    public User User { get; set; }
}
