namespace TrialApplication.Models.DTO
{
    public class BookingDTO
    {

        public int EmployeeId { get; set; }
        public int EventId { get; set; }
        public DateTime BookingDate { get; set; }=DateTime.Now;


    }
}
