using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DoctorPatientApplication.Models;

namespace DoctorPatientApplication.Context
{
    public class HospitalContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=5CD413DKS0\\DEMOINSTANCE;Integrated Security=true;Initial Catalog=HospitalAssignment15Oct;TrustServerCertificate=True");
        }
        public DbSet<Doctor> doctors { get; set; }

        public DbSet<Patient> patients { get; set; }

        public DbSet<Appointment> Appointments { get; set; }

        public DbSet<DoctorPatient> doctorPatients { get; set; }



    }
}
