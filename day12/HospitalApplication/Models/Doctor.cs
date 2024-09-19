namespace HospitalApplication.Models
{
    public class Doctor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Specialization { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Picture { get; set; } // URL or path to the doctor's picture
    }
}
