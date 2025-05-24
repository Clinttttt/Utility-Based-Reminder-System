using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;


namespace First_MVC_webApp.Models
{
    public class CalendarEvent
    {
        [Key]
        public DateTime SelectedDate { get; set; }
        public string? EventTitle { get; set; }
        public string? EventDescription { get; set; }
    }
}
