namespace TrialApplication.Models
{
    public class Event
    {      
        public int EventId { get; set; }
        public string EventName { get; set; }

        public DateTime EventTime { get; set; } 

        public string Location { get; set; }
        public string EventDescription { get; set; }

        public int Capacity { get; set; }

        public string Price { get; set; }

        public ICollection<Booking> Bookings { get; set; }
        public ICollection<Attendance> Attendances { get; set; }


    }
}
