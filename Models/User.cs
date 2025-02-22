using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace software_engineering_product_flowerpower.Models;

public class User
{
    [Key]
    public int ID { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    [JsonIgnore]
    public virtual ICollection<User>? Friends { get; set; }

    [JsonIgnore]
    public virtual ICollection<Group>? Groups { get; set; }

    public virtual ICollection<Photo>? Photos { get; set; }
}