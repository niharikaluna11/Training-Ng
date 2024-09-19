using HospitalApplication.Models;
using Microsoft.AspNetCore.Mvc;

namespace HospitalApplication.Controllers
{
    public class DoctorController : Controller
    {
        private static List<Doctor> doctors = new List<Doctor>
        {
            new Doctor { Id = 1, Name = "Dr. John Smith", Specialization = "Cardiologist", Email = "john.smith@example.com", Phone = "123-456-7890", Picture = "/images/doc1.jpg" },
            new Doctor { Id = 2, Name = "Dr. Cole Doe", Specialization = "Dermatologist", Email = "cole.doe@example.com", Phone = "234-567-8901", Picture = "/images/doc2.jpg" },
            new Doctor { Id = 3, Name = "Dr. Jack Sparrow", Specialization = "Pediatrician", Email = "jack@example.com", Phone = "345-678-9012", Picture = "/images/doc3.jpg" },
            new Doctor { Id = 4, Name = "Dr. Jules Peter", Specialization = "Dermatologist", Email = "jules@example.com", Phone = "234-567-8901", Picture = "/images/doc4.jpg" },
            new Doctor { Id = 5, Name = "Dr. Emily Holland ", Specialization = "Pediatrician", Email = "emily@example.com", Phone = "345-678-9012", Picture = "/images/doc5.jpg" }

        };
        public IActionResult Index()
        {
            ViewData["Patient"] = "Niharika Garg";

            return View(doctors);
        }
    }
}
