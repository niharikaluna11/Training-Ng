namespace TrialApplication.Models
{
    public class Booking
    {
        public int BookingId { get; set; }

        public DateTime BookingDate { get; set; }

        public Employee Employee { get; set; }

        public int EmployeeId { get; set; } 
        public int EventId { get; set; }
        public Event Event { get; set; }    

    }
}
