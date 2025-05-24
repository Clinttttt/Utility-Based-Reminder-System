using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace SmartMessage.Models
{
    [XmlType("Reminder")]
    public class Reminder
    {
        [XmlElement("Id")]
        public int Id { get; set; }

        [Required]
        [XmlElement("Content")]
        public string? Content { get; set; }

        [Required]
        [XmlElement("TriggerTime")]
        public DateTime TriggerTime { get; set; }

        [XmlElement("IsNotified")]
        public bool IsNotified { get; set; } = false;

        [XmlElement("IsDelivered")]
        public bool IsDelivered { get; set; }
    }
}