namespace ClinicApp.Models
{
    public class Doctor : IEquatable<Doctor>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Specialization { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string? Image { get; set; }
        public bool Equals(Doctor? other)
        {
            return this.Id == other.Id;

        }
    }
}
