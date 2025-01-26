using System.ComponentModel.DataAnnotations;

namespace software_engineering_product_flowerpower.Test.Models;

public class FriendRequest
{
    [Key]
    public int ID { get; set; }
    public int ID_User1 { get; set; }
    public User User1 { get; set; }

    public int ID_User2 { get; set; }
    public User User2 { get; set; }

    public bool IsAccepted { get; set; }
    public DateTime RequestDate { get; set; }
}