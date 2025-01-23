using System.ComponentModel.DataAnnotations;

namespace software_engineering_product_flowerpower.Models;

public class Friend
{
    [Key]
    public int ID { get; set; }
    public int ID_User1 { get; set; }
    public User User1 { get; set; }

    public int ID_User2 { get; set; }
    public User User2 { get; set; }

    public DateTime FriendshipDate { get; set; }
}