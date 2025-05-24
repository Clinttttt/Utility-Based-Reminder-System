using First_MVC_webApp.Models;
using Microsoft.EntityFrameworkCore;
using SmartMessage.Models;

namespace First_MVC_webApp.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Category> Categories {get;set;}
        public  DbSet<Todolist> Todolist { get; set; }
        public DbSet<CalendarEvent> CalendarEvents { get; set; }
        public DbSet<Reminder> reminder { get; set; }
        public DbSet<ChatMessage> ChatMessages { get; set; }



   
    }
}
