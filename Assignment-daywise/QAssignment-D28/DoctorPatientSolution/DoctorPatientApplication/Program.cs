using DoctorPatientApplication.Service;

namespace DoctorPatientApplication
{
    public class Program
    {
        HospitalService hospitalService=new HospitalService();
        public Program()
        {
            Console.WriteLine("Hello, Welcome to Hospital :)");
          
        
        }

        static void Main(string[] args)
        {
            Program program = new Program();

        }
    }
}
