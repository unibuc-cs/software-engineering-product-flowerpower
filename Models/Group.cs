// Models/Group.cs
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace software_engineering_product_flowerpower.Models;

public class Group
{
    [Key]
    public int ID { get; set; }
    public string Name { get; set; }
    public int OwnerId { get; set; }

    [ForeignKey("OwnerId")]
    public User Owner { get; set; }

    public virtual ICollection<User> Members { get; set; } = new List<User>();
}