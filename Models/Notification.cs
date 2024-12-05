namespace software_engineering_product_flowerpower.Models;

public class Notification
{
    public int ID { get; set; }
    public int User_ID { get; set; }
    public int Photo_ID { get; set; }
    public User User { get; set; }
    public Photo Photo { get; set; }
}