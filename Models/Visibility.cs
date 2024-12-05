namespace software_engineering_product_flowerpower.Models;

public class Visibility
{
    public int ID_User { get; set; }
    public int ID_Poza { get; set; }
    public User User { get; set; }
    public Photo Photo { get; set; }
}