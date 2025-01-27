namespace software_engineering_product_flowerpower.Test.Models;

public class Visibility
{
    public int User_ID { get; set; }
    public int Photo_ID { get; set; }

    public User User { get; set; }
    public Photo Photo { get; set; }
}