namespace software_engineering_product_flowerpower.Models;

public class Photo
{
    public int ID { get; set; }
    public int User_ID { get; set; }
    public byte[] Blob { get; set; }
    public DateTime UploadTime { get; set; }

    public User User { get; set; }
}