namespace software_engineering_product_flowerpower.Models
{
    public class FriendRequestDto
    {
        public int ID { get; set; }
        public string SenderUsername { get; set; }
        public string ReceiverUsername { get; set; }
        public DateTime RequestDate { get; set; }
        public bool IsAccepted { get; set; }
    }
}