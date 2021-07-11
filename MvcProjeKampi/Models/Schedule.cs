using System;

namespace MvcProjeKampi.Models
{
    public class Schedule
    {
        public string title { get; set; }

        public DateTime start { get; set; }

        public DateTime end { get; set; }

        public bool allDay { get; set; }
    }
}