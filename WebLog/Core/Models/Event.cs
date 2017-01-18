using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebLog.Core.Models
{
    public class Event
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }

        public Event()
        {
            
        }

        public Event(DateTime date, string description)
        {
            Date = date;
            Description = description;
        }
    }
}