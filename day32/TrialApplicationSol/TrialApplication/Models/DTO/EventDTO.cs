namespace TrialApplication.Models.DTO
{
    public class EventDTO
    {
        public string EventName { get; set; }=string.Empty;

        public DateTime EventTime { get; set; }

        public string Location { get; set; } = string.Empty;
        public string EventDescription { get; set; } = string.Empty;

        public int Capacity { get; set; } 

        public string Price { get; set; } = string.Empty;

    }
}
