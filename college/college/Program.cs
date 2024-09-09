namespace college
{
    class Student
    {
        
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }

        public void DisplayID()
        {
            Console.WriteLine("-------------------------------------");
            Console.WriteLine($"Id : {Id}\tName: {Name}");
            Console.WriteLine($"Date Of Birth : {DateOfBirth.ToString("dd-MMMM-yyyy")}");
            Console.WriteLine($"Phone : {Phone}");
            Console.WriteLine($"Email : {Email}");
            Console.WriteLine("-------------------------------------");
        }
    }
        internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            
            Student student = new Student()
            {
                Id = 101,
                Name = "Ramu",
                DateOfBirth = new DateTime(2001, 7, 12),
                Phone = "9876543210",
                Email = "ramu@gmail.com"
            };

            
            student.DisplayID();

            Console.ReadKey();
        }
    }
}
