namespace TrialApplication.Models
{
    public class Attendance
    {

        public int AttendanceId { get; set; }
        public int EmployeeId { get; set; }
        public int EventId { get; set; }
        public DateTime AttendanceDate { get; set; }
        public Employee Employee { get; set; }
        public Event Event { get; set; }

    }
}
