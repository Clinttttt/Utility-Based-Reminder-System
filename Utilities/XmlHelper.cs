using SmartMessage.Models;
using System.Xml;
using System.Xml.Serialization;

namespace SmartMessage.Utilities
{
    public static class XmlHelper
    {
        // Change from private to public
        public static string XmlFilePath => Path.Combine(XmlDirectory, "reminders.xml");

        private static string XmlDirectory => Path.Combine(Directory.GetCurrentDirectory(), "App_Data");

        public static List<Reminder> LoadReminders()
        {
            var reminders = new List<Reminder>();

            if (File.Exists(XmlFilePath))
            {
                var serializer = new XmlSerializer(typeof(List<Reminder>), new XmlRootAttribute("Reminders"));
                using var reader = new StreamReader(XmlFilePath);
                var loadedReminders = serializer.Deserialize(reader) as List<Reminder>;

                if (loadedReminders != null)
                {
                    var maxId = loadedReminders.Any() ? loadedReminders.Max(r => r.Id) : 0;
                    reminders = loadedReminders.Select(r => new Reminder
                    {
                        Id = r.Id == 0 ? ++maxId : r.Id,
                        Content = r.Content,
                        TriggerTime = r.TriggerTime,
                        IsDelivered = r.TriggerTime <= DateTime.Now,
                        IsNotified = r.TriggerTime <= DateTime.Now
                    }).ToList();
                }
            }
            return reminders.OrderBy(r => r.TriggerTime).ToList();
        }

        public static void SaveReminders(List<Reminder> reminders)
        {
            Directory.CreateDirectory(XmlDirectory);
            var settings = new XmlWriterSettings { Indent = true };
            var serializer = new XmlSerializer(typeof(List<Reminder>), new XmlRootAttribute("Reminders"));

            using var writer = XmlWriter.Create(XmlFilePath, settings);
            serializer.Serialize(writer, reminders);
        }
    }
}