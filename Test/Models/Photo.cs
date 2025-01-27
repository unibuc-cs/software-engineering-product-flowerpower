using System.ComponentModel.DataAnnotations;

namespace software_engineering_product_flowerpower.Test.Models;

public class Photo
{
    [Key]
    public int ID { get; set; }
    public int User_ID { get; set; }
    public byte[] Blob { get; set; }
    public DateTime UploadTime { get; set; }

    public User User { get; set; }
}