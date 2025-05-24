namespace SmartMessage.Models
{
    public class ChatMessage
    {
        public int Id { get; set; }
        public string? Content { get; set; }
        public DateTime DeliveryTime { get; set; }
        public bool IsFromUser { get; set; }
    }
}
