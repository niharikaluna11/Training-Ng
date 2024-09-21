using Microsoft.AspNetCore.Mvc;
using ClinicApp.Models;
using System.Security.Cryptography;



namespace ClinicApp.Controllers
{
    public class DoctorController : Controller
    {
         static List<Doctor> doctors = new List<Doctor>
        {
            new Doctor { Id = 1, Name = "Dr. John Smith", Specialization = "Cardiologist", Email = "john.smith@example.com", Phone = "123-456-7890", Image = "/images/doc1.jpg" },
            new Doctor { Id = 2, Name = "Dr. Cole Doe", Specialization = "Dermatologist", Email = "cole.doe@example.com", Phone = "234-567-8901", Image = "/images/doc2.jpg" },
            new Doctor { Id = 3, Name = "Dr. Jack Sparrow", Specialization = "Pediatrician", Email = "jack@example.com", Phone = "345-678-9012", Image = "/images/doc3.jpg" },
            new Doctor { Id = 4, Name = "Dr. Jules Peter", Specialization = "Dermatologist", Email = "jules@example.com", Phone = "234-567-8901", Image = "/images/doc4.jpg" },
            new Doctor { Id = 5, Name = "Dr. Emily Holland ", Specialization = "Pediatrician", Email = "emily@example.com", Phone = "345-678-9012", Image = "/images/doc5.jpg" },
            new Doctor { Id = 6, Name = "Dr. July Holland ", Specialization = "Pediatrician", Email = "July@example.com", Phone = "335-378-9444", Image = "/images/doc5.jpg" }

        };
        public DoctorController() { }
        public IActionResult Index()
        {
            return View(doctors);
        }

        [HttpGet]
        public IActionResult Create()
        {
            Doctor doctor = new Doctor();
            return View(doctor);
        }

    
        [HttpPost]
        public IActionResult Create(Doctor doctor)
        {
            doctor.Id = doctors.Count +1;
            doctors.Add(doctor);
            return RedirectToAction("Index");

        }

        [HttpGet]
        public IActionResult Edit(int did)
        {
            Doctor doctor= doctors.FirstOrDefault(d => d.Id == did);
            return View(doctor);
        }

        [HttpPost]
        public IActionResult Edit(int did, Doctor doctor)
        {

            Doctor olddoctor = doctors.FirstOrDefault(d => d.Id == did);
            if (olddoctor != null)
            {
                olddoctor.Name = doctor.Name;
                olddoctor.Specialization = doctor.Specialization;
                olddoctor.Email = doctor.Email;
                olddoctor.Phone = doctor.Phone;



                if (!string.IsNullOrEmpty(doctor.Image))
                {
                    olddoctor.Image = "/images/" + doctor.Image;
                }

            }

            return RedirectToAction("Index");
        }


        [HttpGet]
        public IActionResult Delete(int did)
        {
            Doctor doctor = doctors.FirstOrDefault(d => d.Id == did);
            return View(doctor);
        }

        [HttpPost]

        public IActionResult Delete(int did, Doctor doctor)
        {
            Doctor doctorToDelete = doctors.FirstOrDefault(p => p.Id == did);
            if (doctorToDelete != null)
            {
                doctors.Remove(doctorToDelete);
            }
            else
            {
                // Handle case where the product is not found
                return NotFound();
            }

            return RedirectToAction("Index");
        }

    }
}
