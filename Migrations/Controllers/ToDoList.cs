using First_MVC_webApp.Data;
using First_MVC_webApp.Models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.ObjectPool;
using Microsoft.Identity.Client;
using Microsoft.VisualBasic;
using SmartMessage.Models;
using SmartMessage.Utilities;
using Syncfusion.EJ2.Buttons;
using Syncfusion.EJ2.Layouts;
using Syncfusion.EJ2.Linq;
using Syncfusion.EJ2.PdfViewer;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using static Azure.Core.HttpHeader;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace First_MVC_webApp.Controllers
{
    public class ToDoList : Controller
    {

        private readonly ApplicationDbContext _db;



        public List<Todolist> tasks = new List<Todolist>();
        public List<CalendarEvent> calendar = new List<CalendarEvent>();
        public List<Reminder> reminders = new List<Reminder>();
        public List<ChatMessage> chatmessage = new List<ChatMessage>();


        public ToDoList(ApplicationDbContext db)
        {
            _db = db;

        }
        // passing the logjc on todolist action
        public IActionResult Todolist()
        {


            if (TempData["reminder_note"] != null)
            {
                ViewBag.reminder_note = TempData["reminder_note"];
                TempData.Keep("reminder_note");
            }
            else
            {
                ViewBag.reminder_note = false;
            }


            var reminders = XmlHelper.LoadReminders();

            ViewBag.Reminders = reminders;
            ViewBag.UpcomingReminders = reminders
                .Where(r => !r.IsDelivered &&
                           r.TriggerTime > DateTime.Now &&
                           r.TriggerTime <= DateTime.Now.AddMinutes(15))
                .ToList();

            ViewBag.ShowXml = TempData["ShowXml"] as bool? ?? false;
            ViewBag.toggle_calendar = TempData["toggle_calendar"] as bool? ?? false;
            ViewBag.Note_Write = TempData["Note_Write"] as bool? ?? false;
            ViewBag.Notes = _db.Todolist.Where(t => t.note_title != null).ToList();


            ViewBag.XmlContent = System.IO.File.Exists(XmlHelper.XmlFilePath)
               ? System.IO.File.ReadAllText(XmlHelper.XmlFilePath)
               : "No XML data available";

            return View(_db.Todolist.Where(t => t.Description != null).ToList());
        }
        // mag create nan task  dre na part
        [HttpPost]
        public IActionResult Createtask(Todolist task)
        {
            if (string.IsNullOrWhiteSpace(task.Description))
            {
                TempData["ErrorMessage"] = "No data!";
                return RedirectToAction("Todolist");
            }
            _db.Todolist.Add(task);
            _db.SaveChanges();
            return RedirectToAction("Todolist");

        }

        // delete note  dre na part
        public IActionResult Delete(int id)
        {
            var task = _db.Todolist.Find(id);
            if (task != null)
            {
                _db.Todolist.Remove(task);
                _db.SaveChanges();
            }
            return RedirectToAction("Todolist");
        }
        [HttpPost]
        public IActionResult MarkAsDone(int id)
        {
            var task = _db.Todolist.Find(id);
            if (task != null)
            {
                task.Isdone = true;


                _db.SaveChanges();
            }

            return RedirectToAction("Todolist");
        }
        // to unmark todolist  dre na part
        public IActionResult UnMmarkAsDone(int id)
        {
            var task = _db.Todolist.Find(id);
            if (task != null)
            {
                task.Isdone = false;
                _db.SaveChanges();
            }
            return RedirectToAction("Todolist");
        }

        // to find if its exist sa eventform data
        public IActionResult GetEvent(string date)
        {
            DateTime eventDate = DateTime.Parse(date);
            var existingEvent = _db.CalendarEvents.FirstOrDefault(e => e.SelectedDate == eventDate);
            if (existingEvent != null)
            {
                return PartialView("_EventData", existingEvent);
            }
            else
            {
                ViewBag.SelectedDate = date;
                return PartialView("_EventForm");
            }
        }
        [HttpPost]
        // save event  dre na part
        public IActionResult SaveEvent(CalendarEvent model)
        {
            if (string.IsNullOrWhiteSpace(model.EventDescription))
            {
                TempData["ErrorMessage"] = "no data!";
                return RedirectToAction("todolist");
            }

            DateTime eventDate = model.SelectedDate;
            var existingEvent = _db.CalendarEvents.FirstOrDefault(e => e.SelectedDate == eventDate);

            if (existingEvent != null)
            {
                existingEvent.EventTitle = model.EventTitle;
                existingEvent.EventDescription = model.EventDescription;
                _db.Entry(existingEvent).State = EntityState.Modified;
            }
            else
            {
                _db.CalendarEvents.Add(model);
            }

            _db.SaveChanges();
            return RedirectToAction("Todolist");
        }
        // to edit data  dre na part
        public IActionResult EditData(string date)
        {

            DateTime eventDate = DateTime.Parse(date);
            var existingEvent = _db.CalendarEvents.FirstOrDefault(e => e.SelectedDate == eventDate);
            if (existingEvent != null)
            {
                ViewBag.SelectedDate = date;
                return PartialView("_EventForm", existingEvent);
            }
            else
            {
                return PartialView("_EventForm");
            }
        }
        [HttpPost]
        // to delete data  dre na part
        public IActionResult DeleteData(string date)
        {

            DateTime eventDate = DateTime.Parse(date);
            var existingEvent = _db.CalendarEvents.FirstOrDefault(e => e.SelectedDate == eventDate);

            if (existingEvent != null)
            {
                _db.CalendarEvents.Remove(existingEvent);
                _db.SaveChanges();
            }
            return RedirectToAction("Todolist");
        }

        [HttpPost]
        // to write a note  dre na part
        public IActionResult Note_Write(string notes_value)
        {
            TempData["reminder_note"] = false;
            TempData["ShowXml"] = false;

            if (notes_value == "shownote")
            {

                TempData["Note_Write"] = true;
            }
            else if (notes_value == "hidenote")
            {
                TempData["Note_Write"] = false;
            }
            return RedirectToAction("Todolist");
        }

        [HttpPost]
        // toggle note  dre na part
        public IActionResult toggle_calendar(string value)
        {
            TempData["ShowXml"] = false;
            TempData["reminder_note"] = false;
            if (value == "showcalendar")
            {
                TempData["toggle_calendar"] = true;
            }
            else if (value == "hide_calendar")
            {
                TempData["toggle_calendar"] = false;
            }
            return RedirectToAction("Todolist");
        }

        [HttpPost]
        // toggle reminder  dre na part
        public IActionResult reminder_note(string reminder_value)
        {
            TempData["ShowXml"] = false;
            if (reminder_value == "showreminder")
            {
                TempData["reminder_note"] = true;
            }
            else if (reminder_value == "hidereminder")
            {
                TempData["reminder_note"] = false;
            }
            return RedirectToAction("Todolist");
        }

        [HttpPost]
        // toggle xml  dre na part
        public IActionResult ToggleXml(string value_xml)
        {
            if (value_xml == "show_xml")
            {
                TempData["ShowXml"] = true;
            }
            else if (value_xml == "hide_xml")
            {
                TempData["ShowXml"] = false;
            }
            return RedirectToAction("Todolist");
        }
        // create note  dre na part
        public IActionResult Create_Note(Todolist note)
        {
            if (!string.IsNullOrWhiteSpace(note.note_title) || !string.IsNullOrWhiteSpace(note.note_description))
            {
                _db.Todolist.Add(note);
                _db.SaveChanges();
            }
            return RedirectToAction("Todolist");
        }
        // delete note  dre na part
        public IActionResult Delete_note(int id_note)
        {
            var note = _db.Todolist.Find(id_note);

            if (note != null)
            {
                _db.Todolist.Remove(note);
                _db.SaveChanges();
                TempData["Message"] = "Note deleted successfully";
            }
            else
            {
                TempData["Message"] = "Note not found";
            }

            return RedirectToAction("Todolist");
        }
        // edit note dre na part
        [HttpPost]
        public IActionResult Edit_note(int id, string note_title, string note_description)
        {
            var note = _db.Todolist.Find(id);

            if (note != null)
            {
                note.note_title = note_title;
                note.note_description = note_description;
                _db.Todolist.Update(note);
                _db.SaveChanges();
            }

            return RedirectToAction("Todolist");
        }
        public IActionResult Card()
        {
            return View();
        }
        // create reminder dre na part
        [HttpPost]
        public IActionResult CreateReminder(string Content, DateTime TriggerTime)
        {
            var reminders = XmlHelper.LoadReminders();

            reminders.Add(new Reminder
            {
                Content = Content,
                TriggerTime = TriggerTime,
                IsDelivered = false,
                IsNotified = false
            });

            XmlHelper.SaveReminders(reminders);
            return RedirectToAction("Todolist");
        }
        // delete reminder dre na part

        [HttpPost]
        public IActionResult DeleteReminder(int id)
        {
            var reminders = XmlHelper.LoadReminders();
            var reminder = reminders.FirstOrDefault(r => r.Id == id);

            if (reminder != null)
            {
                reminders.Remove(reminder);
                XmlHelper.SaveReminders(reminders);
            }
            return RedirectToAction("Todolist");
        }
        // edit reminder 
        [HttpPost]
        public IActionResult EditReminder(int id, string Content, DateTime TriggerTime)
        {
            var reminders = XmlHelper.LoadReminders();
            var reminder = reminders.FirstOrDefault(r => r.Id == id);

            if (reminder != null)
            {
                reminder.Content = Content;
                reminder.TriggerTime = TriggerTime;
                XmlHelper.SaveReminders(reminders);
            }

            return RedirectToAction("Todolist");
        }

    }
}