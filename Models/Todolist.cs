using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace First_MVC_webApp.Models
{
    public class Todolist
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

       
        public int Id { get; set; }

        public string? Description { get; set; }
        public bool Isdone { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime StartDateTime { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime EndDateTime { get; set; }
        [DataType(DataType.Date)]
        public DateOnly EventDate { get; set; }
        [DataType(DataType.Date)]
        public TimeOnly EventTime { get; set; }
        public bool calendar { get; set; }

        public DateTime SelectedDate { get; set; }
        public string? EventTitle { get; set; }
        public string? EventDescription { get; set; }
        public string? note_title { get; set; }
        public string? note_description { get; set; }
    }
}

